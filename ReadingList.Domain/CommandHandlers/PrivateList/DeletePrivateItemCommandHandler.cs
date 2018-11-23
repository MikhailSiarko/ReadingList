using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeletePrivateItemCommandHandler : CommandHandler<DeletePrivateItemCommand>
    {
        public DeletePrivateItemCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override async Task Handle(DeletePrivateItemCommand command)
        {
            var item = await WriteService.GetAsync<PrivateBookListItem>(command.ItemId);

            if (item == null)
            {
                throw new ObjectNotExistException<PrivateBookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ItemId.ToString()
                });
            }

            var accessSpecification = new BookListAccessSpecification(item.BookList);

            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }

            await WriteService.DeleteAsync<PrivateBookListItem>(item.Id);
        }
    }
}