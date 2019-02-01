using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeletePrivateItemCommandHandler : CommandHandler<DeletePrivateItem>
    {
        public DeletePrivateItemCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override async Task Handle(DeletePrivateItem command)
        {
            var item = await WriteService.GetAsync<PrivateBookListItem>(command.ItemId);

            if (item == null)
            {
                throw new ObjectNotExistException<PrivateBookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ItemId.ToString()
                });
            }

            var accessSpecification = new BookListOwnerAccessSpecification(item.BookList);

            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = item.BookListId.ToString()
                });
            }

            await WriteService.DeleteAsync<PrivateBookListItem>(item.Id);
        }
    }
}