using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using PrivateBookListItemWm = ReadingList.WriteModel.Models.PrivateBookListItem;
using BookListWm = ReadingList.WriteModel.Models.BookList;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class AddPrivateItemCommandHandler : AddBookItemCommandHandler<AddPrivateItemCommand>
    {
        public AddPrivateItemCommandHandler(WriteDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<BookList> GetBookList(AddPrivateItemCommand command)
        {
            return await DbContext.BookLists.AsNoTracking()
                       .SingleOrDefaultAsync(p =>
                           p.Owner.Login == command.UserLogin && p.Type == BookListType.Private) ??
                   throw new ObjectNotExistException<BookListWm>(new {email = command.UserLogin});
        }

        protected override BookListItem CreateItem(string title, string author, BookList list)
        {
            return new PrivateBookListItemWm
            {
                Status = BookItemStatus.ToReading,
                LastStatusUpdateDate = DateTime.Now,
                BookListId = list.Id,
                Title = title,
                Author = author
            };
        }

        protected override async Task AddItem(BookListItem item)
        {
            await DbContext.PrivateBookListItems.AddAsync((PrivateBookListItem) item);
        }
    }
}