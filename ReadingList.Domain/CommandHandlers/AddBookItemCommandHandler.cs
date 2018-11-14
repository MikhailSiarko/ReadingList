using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Infrastructure;
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

            if (await DoItemExist(command.BookInfo, listId))
                throw new ObjectAlreadyExistsException<TItem>(new OnExceptionObjectDescriptor
                {
                    ["Title"] = command.BookInfo.Title,
                    ["Author"] = command.BookInfo.Author
                });
            
            var book = await FindOrAddBook(command.BookInfo);
            
            var item = CreateItem(book.Id, listId);

            await SaveAsync(item);

            return Convert(item);
        }

        private async Task<Book> FindOrAddBook(BookInfo bookInfo)
        {
            var book = await _bookFetchHandler.Fetch(
                new GetBookByAuthorAndTitleQuery(bookInfo.Author, bookInfo.Title));

            if (book != null) return book;
            
            book = new Book
            {
                Author = bookInfo.Author,
                Title = bookInfo.Title,
                GenreId = bookInfo.GenreId
            };
                
            await WriteService.SaveAsync(book);

            return book;
        }

        private async Task<bool> DoItemExist(BookInfo bookInfo, int bookListId)
        {
            var item = await _itemFetchHandler.Fetch(new GetBookListItemQuery(bookInfo.Author, bookInfo.Title,
                bookListId));

            return item != null;
        }
        
        protected abstract Task<int> GetBookListId(TCommand command);

        protected abstract TItem CreateItem(int bookId, int listId);
        
        protected abstract Task SaveAsync(TItem item);

        protected abstract TDto Convert(TItem item);
    }
}