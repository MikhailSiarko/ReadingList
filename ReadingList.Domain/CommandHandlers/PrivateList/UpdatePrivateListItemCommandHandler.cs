using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class UpdatePrivateListItemCommandHandler : CommandHandler<UpdatePrivateListItemCommand>
    {
        private readonly WriteDbContext _dbContext;

        public UpdatePrivateListItemCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(UpdatePrivateListItemCommand command)
        {
            var item = await _dbContext.PrivateBookListItems.SingleAsync(i => i.Id == command.ItemId);
            var readingTime = item.ReadingTime +
                ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate, (BookItemStatus) command.Status);
            item.Update(new
            {
                command.Title,
                command.Author,
                command.Status,
                ReadingTime = readingTime,
                LastStatusUpdateDate = DateTime.Now
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}