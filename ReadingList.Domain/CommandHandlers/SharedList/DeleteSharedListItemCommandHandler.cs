using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeleteSharedListItemCommandHandler : CommandHandler<DeleteSharedListItemCommand>
    {
        public DeleteSharedListItemCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override async Task Handle(DeleteSharedListItemCommand command)
        {
            var item = await WriteService.GetAsync<SharedBookListItem>(command.ItemId);

            if (item == null)
            {
                throw new ObjectNotExistException<SharedBookListItem>(
                    new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ItemId.ToString()
                    });
            }

            var accessSpecification = new BookListAccessSpecification(item.BookList);

            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }

            await WriteService.DeleteAsync<SharedBookListItem>(item.Id);
        }
    }
}