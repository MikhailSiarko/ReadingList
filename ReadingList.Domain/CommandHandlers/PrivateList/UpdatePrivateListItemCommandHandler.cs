using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services;
using ReadingList.Domain.Services.Validation;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class UpdatePrivateListItemCommandHandler : UpdateCommandHandler<UpdatePrivateListItemCommand, PrivateBookListItemWm>,
        IValidatable<UpdatePrivateListItemCommand, PrivateBookListItemWm>
    {
        public UpdatePrivateListItemCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService) 
            : base(dbContext, entityUpdateService)
        {
        }

        protected override void Update(PrivateBookListItemWm entity, UpdatePrivateListItemCommand command)
        {
            var readingTime = entity.ReadingTimeInSeconds +
                              ReadingTimeCalculator.Calculate(entity.Status, entity.LastStatusUpdateDate,
                                  (BookItemStatus) command.Status);
            
            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(PrivateBookListItemWm.Title)] = command.BookInfo.Title,
                [nameof(PrivateBookListItemWm.Author)] = command.BookInfo.Author,
                [nameof(PrivateBookListItemWm.Status)] = command.Status,
                [nameof(PrivateBookListItemWm.ReadingTimeInSeconds)] = readingTime,
                [nameof(PrivateBookListItemWm.LastStatusUpdateDate)] = DateTime.Now
            });
        }

        public void Validate(PrivateBookListItemWm entity, UpdatePrivateListItemCommand command)
        {
            PrivateBookListItemStatusValidator.Validate(entity.Status, (BookItemStatus) command.Status);
        }

        protected override async Task<PrivateBookListItemWm> GetEntity(UpdatePrivateListItemCommand command)
        {
            var item = await DbContext.PrivateBookListItems.FirstOrDefaultAsync(i =>
                    i.BookList.Owner.Login == command.UserLogin && i.Id == command.ItemId) ??
                throw new ObjectNotExistException<PrivateBookListItemWm>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ItemId.ToString()
                });
            
            Validate(item, command);

            return item;
        }
    }
}