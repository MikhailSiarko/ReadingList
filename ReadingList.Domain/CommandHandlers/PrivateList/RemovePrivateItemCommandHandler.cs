using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Abstractions;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.WriteModel;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class RemovePrivateItemCommandHandler : CommandHandler<RemovePrivateItemCommand>
    {
        private readonly WriteDbContext _dbContext;

        public RemovePrivateItemCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(RemovePrivateItemCommand command)
        {
            var item = await _dbContext.PrivateBookListItems.SingleAsync(i => i.Id == command.Id);
            _dbContext.PrivateBookListItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}