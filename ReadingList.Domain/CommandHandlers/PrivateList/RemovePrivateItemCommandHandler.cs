using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
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
            var item = await _dbContext.PrivateBookListItems.FirstOrDefaultAsync(i =>
                i.BookList.Owner.Login == command.UserLogin && i.Id == command.Id);
            
            if(item == null)
                throw new ObjectNotExistException($"Item Id: {command.Id.ToString()}");
            
            _dbContext.PrivateBookListItems.Remove(item);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}