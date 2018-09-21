using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Services.Validation;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class AddSharedItemCommandHandler : AddBookItemCommandHandler<AddSharedListItemCommand, SharedBookListItemWm>
    {
        public AddSharedItemCommandHandler(WriteDbContext dbContext) 
            : base(dbContext)
        {
        }

        protected override async Task<BookListWm> GetBookList(AddSharedListItemCommand command)
        {
            var list = await DbContext.BookLists.AsNoTracking().Include(s => s.Owner).Include(s => s.BookListModerators)
                           .ThenInclude(m => m.User).SingleOrDefaultAsync(s =>
                               s.Id == command.ListId && s.Type == BookListType.Shared) ??
                       throw new ObjectNotExistException<BookListWm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.ListId.ToString()
                       });

            BookListAccessValidator.Validate(command.UserLogin, list);

            return list;
        }

        protected override SharedBookListItemWm CreateItem(AddSharedListItemCommand command, int listId)
        {           
            var item = new SharedBookListItemWm
            {
                Author = command.BookInfo.Author,
                Title = command.BookInfo.Title,
                BookListId = listId,
                GenreId = command.BookInfo.GenreId
            };

            var itemTags = DbContext.UpdateOrAddSharedListItemTags(command.Tags, item).RunSync();

            item.SharedBookListItemTags = itemTags.ToList();
            
            return item;
        }

        protected override async Task SaveAsync(SharedBookListItemWm item)
        {
            await DbContext.SharedBookListItems.AddAsync(item);

            await DbContext.SaveChangesAsync();
        }
    }
}