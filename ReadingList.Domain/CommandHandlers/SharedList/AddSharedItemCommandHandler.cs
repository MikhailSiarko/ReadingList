using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class AddSharedItemCommandHandler : CommandHandler<AddSharedListItemCommand>
    {
        private readonly WriteDbContext _dbContext;
        
        public AddSharedItemCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        protected override async Task Handle(AddSharedListItemCommand command)
        {
            var list = await GetBookList(command);
            
            if (await DoItemExist(command.BookInfo, list.Id))
                throw new ObjectAlreadyExistsException<SharedBookListItemWm>(new OnExceptionObjectDescriptor
                {
                    ["Title"] = command.BookInfo.Title,
                    ["Author"] = command.BookInfo.Author
                });
            
            await FindOrAddBook(command.BookInfo);
            
            var item = CreateItem(command, list);
            
            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<BookListWm> GetBookList(AddSharedListItemCommand command)
        {
            return await _dbContext.BookLists.AsNoTracking().SingleOrDefaultAsync(s =>
                       s.Owner.Login == command.UserLogin && s.Id == command.ListId &&
                       s.Type == BookListType.Shared) ??
                   throw new ObjectNotExistForException<BookListWm, UserWm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.ListId.ToString()
                       },
                       new OnExceptionObjectDescriptor
                       {
                           ["Email"] = command.UserLogin
                       });
        }
        
        private async Task<bool> DoItemExist(BookInfo bookInfo, int bookListId)
        {
            return await _dbContext.SharedBookListItems.AnyAsync(x =>
                x.BookListId == bookListId && x.Title == bookInfo.Title && x.Author == bookInfo.Author);
        }
        
        private async Task FindOrAddBook(BookInfo bookInfo)
        {
            var book = await _dbContext.Books.SingleOrDefaultAsync(b =>
                b.Author == bookInfo.Author && b.Title == bookInfo.Title);
            
            if (book == null)
                await _dbContext.Books.AddAsync(new BookWm {Author = bookInfo.Author, Title = bookInfo.Title});
        }

        private async Task<SharedBookListItemWm> CreateItem(AddSharedListItemCommand command, BookListWm list)
        {           
            var item = new SharedBookListItemWm
            {
                Author = command.BookInfo.Author,
                Title = command.BookInfo.Title,
                BookList = list,
                BookListId = list.Id,
                GenreId = command.GenreId
            };

            await _dbContext.UpdateOrAddSharedListItemTags(command.Tags, item);

            return item;
        }
    }
}