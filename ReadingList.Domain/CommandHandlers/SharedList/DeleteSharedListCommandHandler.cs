using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeleteSharedListCommandHandler : CommandHandler<DeleteSharedListCommand>
    {
        public DeleteSharedListCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override async Task Handle(DeleteSharedListCommand command)
        {
            var list = await WriteService.GetAsync<BookList>(command.ListId);
            
            if(list == null)
            {
                throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ListId.ToString()
                });
            }

            var accessSpecification = new BookListAccessSpecification(list);
            
            if(!accessSpecification.SatisfiedBy(command.UserId))
                throw new AccessDeniedException();

            await WriteService.DeleteAsync<BookList>(list.Id);
        }
    }
}