using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Application.Commands;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public abstract class AddBookItemCommandHandler<TCommand, TItem, TDto> : CommandHandler<TCommand, TDto> 
        where TCommand : AddListItemCommand<TDto>
        where TItem : BookListItem
    {
        protected AddBookItemCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
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
            
            await FindOrAddBook(command.BookInfo);
            
            var item = CreateItem(command, listId);

            await SaveAsync(item);

            return Convert(item);
        }

        private async Task FindOrAddBook(BookInfo bookInfo)
        {
            var book = await DbContext.Books.SingleOrDefaultAsync(b =>
                b.Author == bookInfo.Author && b.Title == bookInfo.Title);
            
            if (book == null)
                await DbContext.Books.AddAsync(new Book
                {
                    Author = bookInfo.Author,
                    Title = bookInfo.Title,
                    GenreId = bookInfo.GenreId
                });
        }

        private async Task<bool> DoItemExist(BookInfo bookInfo, int bookListId)
        {
            return await DbContext.Set<TItem>().AnyAsync(x =>
                x.BookListId == bookListId && x.Title == bookInfo.Title && x.Author == bookInfo.Author);
        }
        
        protected abstract Task<int> GetBookListId(TCommand command);

        protected abstract TItem CreateItem(TCommand command, int listId);
        
        protected abstract Task SaveAsync(TItem item);

        protected abstract TDto Convert(TItem item);
    }
}