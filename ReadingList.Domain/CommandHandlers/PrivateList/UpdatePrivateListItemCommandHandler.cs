using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
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
            var item = await _dbContext.PrivateBookListItems.FirstOrDefaultAsync(i =>
                i.BookList.Owner.Login == command.UserLogin && i.Id == command.ItemId);

            if (item == null)
                throw new ObjectNotExistException($"ItemId: {command.ItemId.ToString()}");
            
            PrivateBookListItemStatusValidator.Validate(item.Status, (BookItemStatus) command.Status);
            
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