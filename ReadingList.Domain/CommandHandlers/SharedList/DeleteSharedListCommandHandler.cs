using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeleteSharedListCommandHandler : CommandHandler<DeleteSharedListCommand>
    {
        private readonly WriteDbContext _context;

        public DeleteSharedListCommandHandler(WriteDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(DeleteSharedListCommand command)
        {
            var list =
                await _context.BookLists.Include(s => s.Owner)
                    .SingleOrDefaultAsync(BookListFilterExpressions.FindSharedBookList(command.ListId)) ??
                throw new ObjectNotExistException<BookListWm>(new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ListId.ToString()
                    });
            
            if(list.Owner.Login != command.UserLogin)
                throw new AccessDeniedException();

            var items = await _context.SharedBookListItems
                .Where(BookListItemFilterExpressions.ItemBelongsToList<SharedBookListItemWm>(command.ListId)).ToListAsync();
            
            _context.SharedBookListItems.RemoveRange(items);
            
            _context.BookLists.Remove(list);
            
            await _context.SaveChangesAsync();
        }
    }
}