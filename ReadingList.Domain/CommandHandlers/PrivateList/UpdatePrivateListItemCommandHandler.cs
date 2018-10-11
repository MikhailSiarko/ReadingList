﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.Domain.Infrastructure.Filters.ValidationFilters;
using ReadingList.Domain.Services;
using ReadingList.Domain.Services.Validation;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdatePrivateListItemCommandHandler
        : UpdateCommandHandler<UpdatePrivateListItemCommand, PrivateBookListItemWm, PrivateBookListItemDto>
    {
        public UpdatePrivateListItemCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService)
            : base(dbContext, entityUpdateService)
        {
        }

        protected override PrivateBookListItemDto Convert(PrivateBookListItemWm entity)
        {
            return Mapper.Map<PrivateBookListItemWm, PrivateBookListItemDto>(entity);
        }

        protected override void Update(PrivateBookListItemWm entity, UpdatePrivateListItemCommand command)
        {
            var readingTime = entity.ReadingTimeInSeconds +
                              ReadingTimeCalculator.Calculate(entity.Status, entity.LastStatusUpdateDate,
                                  (BookItemStatus)command.Status);

            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(PrivateBookListItemWm.Title)] = command.BookInfo.Title,
                [nameof(PrivateBookListItemWm.Author)] = command.BookInfo.Author,
                [nameof(PrivateBookListItemWm.Status)] = command.Status,
                [nameof(PrivateBookListItemWm.ReadingTimeInSeconds)] = readingTime,
                [nameof(PrivateBookListItemWm.LastStatusUpdateDate)] = DateTime.Now
            });
        }

        protected override async Task<PrivateBookListItemWm> GetEntity(UpdatePrivateListItemCommand command)
        {
            var itemQuery =
                DbContext.PrivateBookListItems.Where(
                    EntityFilterExpressions.FindEntity<PrivateBookListItemWm>(command.ItemId));

            var item = await itemQuery.SingleOrDefaultAsync() ??
                       throw new ObjectNotExistException<PrivateBookListItemWm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.ItemId.ToString()
                       });

            if (!await itemQuery.AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOfItem<PrivateBookListItemWm>(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            PrivateBookListItemStatusValidator.Validate(item.Status, (BookItemStatus)command.Status);

            return item;
        }
    }
}