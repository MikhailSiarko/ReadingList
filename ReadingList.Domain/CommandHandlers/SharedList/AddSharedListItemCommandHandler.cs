using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class AddSharedListItemCommandHandler : AddBookItemCommandHandler<AddSharedListItemCommand, SharedBookListItemWm>
    {
        public AddSharedListItemCommandHandler(WriteDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<BookListWm> GetBookList(AddSharedListItemCommand command)
        {
            return await DbContext.BookLists.AsNoTracking().SingleOrDefaultAsync(s =>
                       s.Owner.Login == command.UserLogin && s.Id == command.ListId &&
                       s.Type == BookListType.Shared) ??
                   throw new ObjectNotExistException<BookListWm>(new {email = command.UserLogin});
        }

        protected override SharedBookListItemWm CreateItem(string title, string author, BookListWm list)
        {
            return new SharedBookListItemWm
            {
                Author = author,
                Title = title,
                BookList = list,
                BookListId = list.Id
            };
        }
    }
}