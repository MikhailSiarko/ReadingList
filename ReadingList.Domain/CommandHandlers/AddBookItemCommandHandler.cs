using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class AddBookItemCommandHandler<TCommand, TItem> : CommandHandler<TCommand> 
        where TCommand : AddPrivateItemCommand 
        where TItem : BookListItemWm
    {
        protected readonly WriteDbContext DbContext;

        protected AddBookItemCommandHandler(WriteDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected sealed override async Task Handle(TCommand command)
        {
            var bookList = await GetBookList(command);

            if (await DoItemExist(command.Title, command.Author, bookList.Id))
                throw new ObjectAlreadyExistsException<TItem>(new {title = command.Title, author = command.Author});
            
            await FindOrAddBook(command.Title, command.Author);
            
            var item = CreateItem(command.Title, command.Author, bookList);
            
            await DbContext.AddAsync(item);
            await DbContext.SaveChangesAsync();
        }

        private async Task FindOrAddBook(string title, string author)
        {
            var book = await DbContext.Books.SingleOrDefaultAsync(b =>
                b.Author == author && b.Title == title);
            
            if (book == null)
                await DbContext.Books.AddAsync(new BookWm {Author = author, Title = title});
        }

        private async Task<bool> DoItemExist(string title, string author, int bookListId)
        {
            return await DbContext.Set<TItem>().AnyAsync(x =>
                x.BookListId == bookListId && x.Title == title && x.Author == author);
        }
        
        protected abstract Task<BookListWm> GetBookList(TCommand command);

        protected abstract TItem CreateItem(string title, string author, BookListWm list);
    }
}