using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Application.Infrastructure.Filters.ValidationFilters;
using ReadingList.Domain.Infrastructure;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class UpdateSharedListItemCommandHandler 
        : UpdateCommandHandler<UpdateSharedListItemCommand, SharedBookListItem, SharedBookListItemDto>
    {
        public UpdateSharedListItemCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override SharedBookListItemDto Convert(SharedBookListItem entity)
        {
            return Mapper.Map<SharedBookListItem, SharedBookListItemDto>(entity);
        }

        protected override void Update(SharedBookListItem entity, UpdateSharedListItemCommand command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(SharedBookListItem.Title)] = command.BookInfo.Title,
                [nameof(SharedBookListItem.Author)] = command.BookInfo.Author,
                [nameof(SharedBookListItem.GenreId)] = command.BookInfo.GenreId,
                [nameof(SharedBookListItem.SharedBookListItemTags)] =
                    DbContext.UpdateOrAddSharedListItemTags(command.Tags, entity).RunSync().ToList()
            });
        }

        protected override async Task<SharedBookListItem> GetEntity(UpdateSharedListItemCommand command)
        {
            var item = await DbContext.SharedBookListItems
                           .Include(i => i.SharedBookListItemTags)
                           .ThenInclude(t => t.Tag)
                           .SingleOrDefaultAsync(
                               EntityFilterExpressions.WithId<SharedBookListItem>(command.ItemId) &&
                               BookListItemFilterExpressions.BelongsToListWithId<SharedBookListItem>(command.ListId)) ??
                       throw new ObjectNotExistException<SharedBookListItem>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.ItemId.ToString()
                       });

            if (!await DbContext.BookLists.Where(BookListFilterExpressions.SharedBookListWithId(command.ListId))
                .AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOrModerator(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            return item;
        }
    }
}