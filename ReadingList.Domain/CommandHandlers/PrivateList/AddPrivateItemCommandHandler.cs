using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class AddPrivateItemCommandHandler : AddBookItemCommandHandler<AddPrivateItemCommand, PrivateBookListItemWm>
    {
        public AddPrivateItemCommandHandler(WriteDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<BookListWm> GetBookList(AddPrivateItemCommand command)
        {
            return await DbContext.BookLists.AsNoTracking()
                       .SingleOrDefaultAsync(p =>
                           p.Owner.Login == command.UserLogin && p.Type == BookListType.Private) ??
                   throw new ObjectNotExistForException<BookListWm, UserWm>(null, new OnExceptionObjectDescriptor
                   {
                       ["Email"] = command.UserLogin
                   });
        }

        protected override PrivateBookListItemWm CreateItem(AddPrivateItemCommand command, int listId)
        {
            return new PrivateBookListItemWm
            {
                Status = BookItemStatus.ToReading,
                LastStatusUpdateDate = DateTime.Now,
                BookListId = listId,
                Title = command.BookInfo.Title,
                Author = command.BookInfo.Author
            };
        }

        protected override async Task SaveAsync(PrivateBookListItemWm item)
        {
            await DbContext.PrivateBookListItems.AddAsync(item);

            await DbContext.SaveChangesAsync();
        }
    }
}