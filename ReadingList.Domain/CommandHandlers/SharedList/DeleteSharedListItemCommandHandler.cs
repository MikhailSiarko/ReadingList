using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeleteSharedListItemCommandHandler : CommandHandler<DeleteSharedListItem>
    {
        public DeleteSharedListItemCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override async Task Handle(DeleteSharedListItem command)
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
                throw new AccessDeniedException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = item.BookListId.ToString()
                });
            }

            await WriteService.DeleteAsync<SharedBookListItem>(item.Id);
        }
    }
}