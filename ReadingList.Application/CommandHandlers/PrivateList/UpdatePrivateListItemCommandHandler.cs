using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Application.Infrastructure.Filters.ValidationFilters;
using ReadingList.Application.Services;
using ReadingList.Application.Services.Validation;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class UpdatePrivateListItemCommandHandler
        : UpdateCommandHandler<UpdatePrivateListItemCommand, PrivateBookListItem, PrivateBookListItemDto>
    {
        public UpdatePrivateListItemCommandHandler(ApplicationDbContext dbContext, IEntityUpdateService entityUpdateService)
            : base(dbContext, entityUpdateService)
        {
        }

        protected override PrivateBookListItemDto Convert(PrivateBookListItem entity)
        {
            return Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(entity);
        }

        protected override void Update(PrivateBookListItem entity, UpdatePrivateListItemCommand command)
        {
            var readingTime = ReadingTimeCalculator.Calculate(entity.ReadingTimeInSeconds, entity.Status, 
                entity.LastStatusUpdateDate, (BookItemStatus)command.Status);

            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(PrivateBookListItem.Title)] = command.BookInfo.Title,
                [nameof(PrivateBookListItem.Author)] = command.BookInfo.Author,
                [nameof(PrivateBookListItem.Status)] = command.Status,
                [nameof(PrivateBookListItem.ReadingTimeInSeconds)] = readingTime,
                [nameof(PrivateBookListItem.LastStatusUpdateDate)] = DateTime.Now
            });
        }

        protected override async Task<PrivateBookListItem> GetEntity(UpdatePrivateListItemCommand command)
        {
            var itemQuery =
                DbContext.PrivateBookListItems.Where(
                    EntityFilterExpressions.WithId<PrivateBookListItem>(command.ItemId));

            var item = await itemQuery.SingleOrDefaultAsync() ??
                       throw new ObjectNotExistException<PrivateBookListItem>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.ItemId.ToString()
                       });

            if (!await itemQuery.AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOfItem<PrivateBookListItem>(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            PrivateBookListItemStatusValidator.Validate(item.Status, (BookItemStatus)command.Status);

            return item;
        }
    }
}