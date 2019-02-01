using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeleteSharedListCommandHandler : CommandHandler<DeleteSharedList>
    {
        public DeleteSharedListCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override async Task Handle(DeleteSharedList command)
        {
            var list = await WriteService.GetAsync<BookList>(command.ListId);

            if (list == null)
            {
                throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ListId.ToString()
                });
            }

            var accessSpecification = new BookListOwnerAccessSpecification(list);

            if (!accessSpecification.SatisfiedBy(command.UserId))
                throw new AccessDeniedException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = list.Id.ToString()
                });

            await WriteService.DeleteAsync<BookList>(list.Id);
        }
    }
}