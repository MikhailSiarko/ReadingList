using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class DeletePrivateItemCommandHandler : CommandHandler<DeletePrivateItemCommand>
    {
        private readonly WriteDbContext _dbContext;

        public DeletePrivateItemCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePrivateItemCommand command)
        {
            var item = await _dbContext.PrivateBookListItems.FirstOrDefaultAsync(i =>
                           i.BookList.Owner.Login == command.UserLogin && i.Id == command.Id) ??
                       throw new ObjectNotExistException<PrivateBookListItemWm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.Id.ToString()
                       });
            
            _dbContext.PrivateBookListItems.Remove(item);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}