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
            var item = await _dbContext.PrivateBookListItems.SingleAsync(i => i.Id == command.ItemId);
            item.Update(command);
            await _dbContext.SaveChangesAsync();
        }
    }
}