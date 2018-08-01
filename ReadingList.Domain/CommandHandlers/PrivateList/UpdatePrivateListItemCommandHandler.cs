using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using PrivateBookListItemWm = ReadingList.WriteModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class UpdatePrivateListItemCommandHandler : CommandHandler<UpdatePrivateListItemCommand>
    {
        private readonly WriteDbContext _dbContext;
        private readonly IEntityUpdateService _entityUpdateService;

        public UpdatePrivateListItemCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService)
        {
            _dbContext = dbContext;
            _entityUpdateService = entityUpdateService;
        }

        protected override async Task Handle(UpdatePrivateListItemCommand command)
        {
            var item = await _dbContext.PrivateBookListItems.FirstOrDefaultAsync(i =>
                           i.BookList.Owner.Login == command.UserLogin && i.Id == command.ItemId) ??
                       throw new ObjectNotExistException<PrivateBookListItemWm>(new {id = command.ItemId});
            
            PrivateBookListItemStatusValidator.Validate(item.Status, (BookItemStatus) command.Status);
            
            var readingTime = item.ReadingTime +
                ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate, (BookItemStatus) command.Status);
            
            _entityUpdateService.Update(item, new Dictionary<string, object>
            {
                [nameof(PrivateBookListItemWm.Title)] = command.Title,
                [nameof(PrivateBookListItemWm.Author)] = command.Author,
                [nameof(PrivateBookListItemWm.Status)] = command.Status,
                [nameof(PrivateBookListItemWm.ReadingTime)] = readingTime,
                [nameof(PrivateBookListItemWm.LastStatusUpdateDate)] = DateTime.Now
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}