using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Application.Commands;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class DeleteSharedListCommandHandler : CommandHandler<DeleteSharedListCommand>
    {
        public DeleteSharedListCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        protected override async Task Handle(DeleteSharedListCommand command)
        {
            var list =
                await DbContext.BookLists.Include(s => s.Owner)
                    .SingleOrDefaultAsync(BookListFilterExpressions.SharedBookListWithId(command.ListId)) ??
                throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ListId.ToString()
                    });
            
            if(list.Owner.Login != command.UserLogin)
                throw new AccessDeniedException();

            var items = await DbContext.SharedBookListItems
                .Where(BookListItemFilterExpressions.BelongsToListWithId<SharedBookListItem>(command.ListId)).ToListAsync();
            
            DbContext.SharedBookListItems.RemoveRange(items);
            
            DbContext.BookLists.Remove(list);
            
            await DbContext.SaveChangesAsync();
        }
    }
}