using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using PrivateBookListItemWm = ReadingList.WriteModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class AddPrivateItemCommandHandler : CommandHandler<AddPrivateItemCommand>
    {
        private readonly WriteDbContext _dbContext;

        public AddPrivateItemCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(AddPrivateItemCommand command)
        {
            var list = await _dbContext.BookLists.AsNoTracking()
                .Where(l => l.Owner.Login == command.UserLogin && l.Type == BookListType.Private)
                .FirstOrDefaultAsync();

            if (list == null)
                throw new ObjectNotExistException($"Private list for user {command.UserLogin}");
            
            var book = await _dbContext.Books.SingleOrDefaultAsync(b =>
                b.Author == command.Author && b.Title == command.Title);
            
            if (book == null)
                await _dbContext.Books.AddAsync(new Book {Author = command.Author, Title = command.Title});
            
            var listItem = new PrivateBookListItemWm
            {
                Status = BookItemStatus.ToReading,
                LastStatusUpdateDate = DateTime.Now,
                BookListId = list.Id,
                Title = command.Title,
                Author = command.Author
            };
            
            await _dbContext.PrivateBookListItems.AddAsync(listItem);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}