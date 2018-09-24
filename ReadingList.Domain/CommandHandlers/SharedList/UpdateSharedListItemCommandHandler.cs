using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.Domain.Infrastructure.Filters.ValidationFilters;
using ReadingList.Domain.Services;
using ReadingList.ReadModel.Models;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class UpdateSharedListItemCommandHandler : UpdateCommandHandler<UpdateSharedListItemCommand, SharedBookListItemWm>
    {
        public UpdateSharedListItemCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService) 
            : base(dbContext, entityUpdateService)
        {
        }

        protected override void Update(SharedBookListItemWm entity, UpdateSharedListItemCommand command)
        {
            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(SharedBookListItemWm.Title)] = command.BookInfo.Title,
                [nameof(SharedBookListItemWm.Author)] = command.BookInfo.Author,
                [nameof(SharedBookListItemWm.GenreId)] = command.BookInfo.GenreId,
                [nameof(SharedBookListItemWm.SharedBookListItemTags)] = DbContext.UpdateOrAddSharedListItemTags(command.Tags, entity).RunSync().ToList()
            });
        }

        protected override async Task<SharedBookListItemWm> GetEntity(UpdateSharedListItemCommand command)
        {
            var item = await DbContext.SharedBookListItems
                           .Include(i => i.SharedBookListItemTags)
                           .SingleOrDefaultAsync(
                               EntityFilterExpressions.FindEntity<SharedBookListItemWm>(command.ItemId) &&
                               BookListItemFilterExpressions.ItemBelongsToList<SharedBookListItemWm>(command.ListId)) ??
                       throw new ObjectNotExistException<SharedBookListItemWm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.ItemId.ToString()
                       });

            if (!await DbContext.BookLists.Where(BookListFilterExpressions.FindSharedBookList(command.ListId))
                .AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOrModerator(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            return item;
        }
    }
}