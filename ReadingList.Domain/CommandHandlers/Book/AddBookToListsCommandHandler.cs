using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddBookToListsCommandHandler : CommandHandler<AddBookToLists>
    {
        private readonly IFetchHandler<GetListsByIds, IEnumerable<BookList>> _listsFetchHandler;

        private readonly IFetchHandler<GetItemsByBookAndListIds, IEnumerable<BookListItem>> _itemsFetchHandler;
        public AddBookToListsCommandHandler(IDataStorage writeService, IFetchHandler<GetListsByIds,
            IEnumerable<BookList>> listsFetchHandler, IFetchHandler<GetItemsByBookAndListIds,
            IEnumerable<BookListItem>> itemsFetchHandler) : base(writeService)
        {
            _listsFetchHandler = listsFetchHandler;
            _itemsFetchHandler = itemsFetchHandler;
        }

        protected override async Task Handle(AddBookToLists command)
        {
            var lists = await _listsFetchHandler.Handle(new GetListsByIds(command.ListsIds));

            var items =
                (await _itemsFetchHandler.Handle(new GetItemsByBookAndListIds(command.ListsIds, command.BookId)))
                .ToList();

            var newItems = new List<BookListItem>();

            foreach (var list in lists)
            {
                Validate(list, command.UserId, items, command.BookId);
                newItems.Add(CreateItem(list, command));
            }

            await WriteService.SaveBatchAsync(newItems);
        }

        private static BookListItem CreateItem(BookList list, AddBookToLists command)
        {
            BookListItem item;

            if (list.Type == BookListType.Shared)
            {
                item = new SharedBookListItem
                {
                    BookListId = list.Id,
                    BookId = command.BookId
                };
            }
            else
            {
                item = new PrivateBookListItem
                {
                    BookId = command.BookId,
                    BookListId = list.Id
                };
            }

            return item;
        }

        private static void Validate(BookList list, int userId, IEnumerable<BookListItem> items, int bookId)
        {
            var accessSpecification = new BookListAccessSpecification(list);

            if (!accessSpecification.SatisfiedBy(userId))
            {
                throw new AccessDeniedException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = list.Id.ToString()
                });
            }

            if (items.Any(i => i.BookId == bookId && i.BookListId == list.Id))
            {
                throw new ObjectAlreadyExistsException<BookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = bookId.ToString()
                });
            }
        }
    }
}