using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Application.Commands;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Application.Infrastructure.Filters.ValidationFilters;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class DeleteSharedListItemCommandHandler : CommandHandler<DeleteSharedListItemCommand>
    {
        public DeleteSharedListItemCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        protected override async Task Handle(DeleteSharedListItemCommand command)
        {
            var item = await DbContext.SharedBookListItems.SingleOrDefaultAsync(
                           EntityFilterExpressions.WithId<SharedBookListItem>(command.ItemId) &&
                           BookListItemFilterExpressions.BelongsToListWithId<SharedBookListItem>(command.ListId)) ??
                       throw new ObjectNotExistException<SharedBookListItem>(
                           new OnExceptionObjectDescriptor
                           {
                               ["Id"] = command.ItemId.ToString()
                           });

            if (!await DbContext.BookLists.Where(BookListFilterExpressions.SharedBookListWithId(command.ListId))
                .AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOrModerator(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            DbContext.SharedBookListItems.Remove(item);

            await DbContext.SaveChangesAsync();
        }
    }
}