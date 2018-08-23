using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class AddSharedListItemCommandHandler : AddBookItemCommandHandler<AddSharedListItemCommand>
    {
        public AddSharedListItemCommandHandler(WriteDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<BookList> GetBookList(AddSharedListItemCommand command)
        {
            return await DbContext.BookLists.AsNoTracking().SingleOrDefaultAsync(s =>
                       s.Owner.Login == command.UserLogin && s.Id == command.ListId &&
                       s.Type == BookListType.Shared) ??
                   throw new ObjectNotExistException<BookList>(new {email = command.UserLogin});
        }

        protected override BookListItem CreateItem(string title, string author, BookList list)
        {
            return new SharedBookListItem
            {
                Author = author,
                Title = title,
                BookList = list,
                BookListId = list.Id
            };
        }

        protected override async Task AddItem(BookListItem item)
        {
            await DbContext.SharedBookListItems.AddAsync((SharedBookListItem) item);
        }
    }
}