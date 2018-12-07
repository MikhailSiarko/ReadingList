using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddSharedItemCommandHandler
        : AddBookItemCommandHandler<AddSharedListItem, SharedBookListItem, SharedBookListItemDto>
    {
        public AddSharedItemCommandHandler(IDataStorage writeService,
            IFetchHandler<GetBookListItem, SharedBookListItem> itemFetchHandler)
            : base(writeService, itemFetchHandler)
        {
        }

        protected override async Task<int> GetBookListId(AddSharedListItem command)
        {
            var list = await WriteService.GetAsync<BookList>(command.ListId);

            if (list == null)
            {
                throw new ObjectNotExistException<BookList>(
                    new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ListId.ToString()
                    });
            }

            var accessSpecification = new BookListAccessSpecification(list);

            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }

            return command.ListId;
        }

        protected override SharedBookListItem CreateItem(Book book, int listId)
        {
            var item = new SharedBookListItem
            {
                BookListId = listId,
                BookId = book.Id,
                Book = book
            };

            return item;
        }

        protected override async Task SaveAsync(SharedBookListItem item)
        {
            await WriteService.SaveAsync(item);
        }

        protected override SharedBookListItemDto Convert(SharedBookListItem item)
        {
            return Mapper.Map<SharedBookListItem, SharedBookListItemDto>(item);
        }
    }
}