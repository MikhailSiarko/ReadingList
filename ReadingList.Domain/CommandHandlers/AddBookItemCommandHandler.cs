using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class AddBookItemCommandHandler<TCommand, TItem, TDto> : CommandHandler<TCommand, TDto>
        where TCommand : AddListItem<TDto>
        where TItem : BookListItem
    {
        private readonly IFetchHandler<GetBookListItem, TItem> _itemFetchHandler;

        protected AddBookItemCommandHandler(IDataStorage writeService,
            IFetchHandler<GetBookListItem, TItem> itemFetchHandler) : base(writeService)
        {
            _itemFetchHandler = itemFetchHandler;
        }

        protected sealed override async Task<TDto> Handle(TCommand command)
        {
            var listId = await GetBookListId(command);

            var book = await WriteService.GetAsync<Book>(command.BookId);

            if (await DoItemExist(command.BookId, listId))
                throw new ObjectAlreadyExistsException<TItem>(new OnExceptionObjectDescriptor
                {
                    ["Author"] = book.Author,
                    ["Title"] = book.Title
                });

            var item = CreateItem(book, listId);

            await SaveAsync(item);

            return Convert(item);
        }

        private async Task<bool> DoItemExist(int bookId, int bookListId)
        {
            var item = await _itemFetchHandler.Handle(new GetBookListItem(bookId, bookListId));

            return item != null;
        }

        protected abstract Task<int> GetBookListId(TCommand command);

        protected abstract TItem CreateItem(Book book, int listId);

        protected abstract Task SaveAsync(TItem item);

        protected abstract TDto Convert(TItem item);
    }
}