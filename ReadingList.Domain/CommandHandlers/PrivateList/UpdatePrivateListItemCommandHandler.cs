using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Infrastructure.Extensions;
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
            var item = await _dbContext.PrivateBookListItems.Include(i => i.ReadingJournalRecords)
                .SingleAsync(i => i.Id == command.ItemId);
            var newStatus = (BookItemStatus) command.Status;
            if (item.Status != newStatus)
            {
                var lastRecord = item.ReadingJournalRecords.LastOrDefault();
                if (lastRecord == null)
                {
                    item.ReadingJournalRecords.Add(new ReadingJournalRecord
                    {
                        StatusChangedDate = DateTime.Now,
                        StatusSetTo = newStatus,
                        ItemId = item.Id
                    });
                }
                else
                {
                    // TODO Add time calculation logic
                }
            }
            item.Update(command);
            await _dbContext.SaveChangesAsync();
        }
    }
}