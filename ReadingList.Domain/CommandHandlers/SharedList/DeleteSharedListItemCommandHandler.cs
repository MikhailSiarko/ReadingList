using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.Domain.Infrastructure.Filters.ValidationFilters;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class DeleteSharedListItemCommandHandler : CommandHandler<DeleteSharedListItemCommand>
    {
        private readonly WriteDbContext _context;

        public DeleteSharedListItemCommandHandler(WriteDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(DeleteSharedListItemCommand command)
        {
            var item = await _context.SharedBookListItems.SingleOrDefaultAsync(
                           EntityFilterExpressions.FindEntity<SharedBookListItemWm>(command.ItemId) &&
                           BookListItemFilterExpressions.ItemBelongsToList<SharedBookListItemWm>(command.ListId)) ??
                       throw new ObjectNotExistException<SharedBookListItemWm>(
                           new OnExceptionObjectDescriptor
                           {
                               ["Id"] = command.ItemId.ToString()
                           });

            if (!await _context.BookLists.Where(BookListFilterExpressions.FindSharedBookList(command.ListId))
                .AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOrModerator(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            _context.SharedBookListItems.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}