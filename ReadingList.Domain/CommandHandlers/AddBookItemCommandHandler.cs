using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class AddBookItemCommandHandler<TCommand, TItem, TDto> : CommandHandler<TCommand, TDto>
        where TCommand : AddListItemCommand<TDto>
        where TItem : BookListItem
    {
        private readonly IFetchHandler<GetBookByAuthorAndTitleQuery, Book> _bookFetchHandler;

        private readonly IFetchHandler<GetBookListItemQuery, TItem> _itemFetchHandler;

        protected AddBookItemCommandHandler(IDataStorage writeService,
            IFetchHandler<GetBookByAuthorAndTitleQuery, Book> bookFetchHandler,
            IFetchHandler<GetBookListItemQuery, TItem> itemFetchHandler) : base(writeService)
        {
            _bookFetchHandler = bookFetchHandler;
            _itemFetchHandler = itemFetchHandler;
        }

        protected sealed override async Task<TDto> Handle(TCommand command)
        {
            var listId = await GetBookListId(command);

            if (await DoItemExist(command.BookId, listId))
                throw new ObjectAlreadyExistsException<TItem>(new OnExceptionObjectDescriptor
                {
                    ["Book Id"] = command.BookId.ToString()
                });

            var book = await WriteService.GetAsync<Book>(command.BookId);

            var item = CreateItem(book, listId);

            await SaveAsync(item);

            return Convert(item);
        }

        private async Task<bool> DoItemExist(int bookId, int bookListId)
        {
            var item = await _itemFetchHandler.Fetch(new GetBookListItemQuery(bookId, bookListId));

            return item != null;
        }

        protected abstract Task<int> GetBookListId(TCommand command);

        protected abstract TItem CreateItem(Book book, int listId);

        protected abstract Task SaveAsync(TItem item);

        protected abstract TDto Convert(TItem item);
    }
}