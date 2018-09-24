using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class AddBookItemCommandHandler<TCommand, TItem> : CommandHandler<TCommand> 
        where TCommand : AddListItemCommand
        where TItem : BookListItemWm
    {
        protected readonly WriteDbContext DbContext;

        protected AddBookItemCommandHandler(WriteDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected sealed override async Task Handle(TCommand command)
        {
            var list = await GetBookList(command);

            if (await DoItemExist(command.BookInfo, list.Id))
                throw new ObjectAlreadyExistsException<TItem>(new OnExceptionObjectDescriptor
                {
                    ["Title"] = command.BookInfo.Title,
                    ["Author"] = command.BookInfo.Author
                });
            
            await FindOrAddBook(command.BookInfo);
            
            var item = CreateItem(command, list);

            await SaveAsync(item);
        }

        private async Task FindOrAddBook(BookInfo bookInfo)
        {
            var book = await DbContext.Books.SingleOrDefaultAsync(b =>
                b.Author == bookInfo.Author && b.Title == bookInfo.Title);
            
            if (book == null)
                await DbContext.Books.AddAsync(new BookWm {Author = bookInfo.Author, Title = bookInfo.Title, GenreId = bookInfo.GenreId});
        }

        private async Task<bool> DoItemExist(BookInfo bookInfo, int bookListId)
        {
            return await DbContext.Set<TItem>().AnyAsync(x =>
                x.BookListId == bookListId && x.Title == bookInfo.Title && x.Author == bookInfo.Author);
        }
        
        protected abstract Task<BookListWm> GetBookList(TCommand command);

        protected abstract TItem CreateItem(TCommand command, BookListWm list);
        
        protected abstract Task SaveAsync(TItem item);
    }
}