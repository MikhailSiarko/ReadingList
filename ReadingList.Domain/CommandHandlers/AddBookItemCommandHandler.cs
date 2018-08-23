using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class AddBookItemCommandHandler<TCommand> : CommandHandler<TCommand> where TCommand : AddPrivateItemCommand
    {
        protected readonly WriteDbContext DbContext;

        protected AddBookItemCommandHandler(WriteDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected sealed override async Task Handle(TCommand command)
        {
            var bookList = await GetBookList(command);
            await FindOrAddBook(command.Title, command.Author);
            var item = CreateItem(command.Title, command.Author, bookList);
            await AddItem(item);
            await DbContext.SaveChangesAsync();
        }


        private async Task FindOrAddBook(string title, string author)
        {
            var book = await DbContext.Books.SingleOrDefaultAsync(b =>
                b.Author == author && b.Title == title);
            
            if (book == null)
                await DbContext.Books.AddAsync(new Book {Author = author, Title = title});
        }
        
        protected abstract Task<BookList> GetBookList(TCommand command);

        protected abstract BookListItem CreateItem(string title, string author, BookList list);

        protected abstract Task AddItem(BookListItem item);
    }
}