using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class UpdatePrivateListCommandHandler : CommandHandler<UpdatePrivateListCommand>
    {
        private readonly WriteDbContext _dbContext;

        public UpdatePrivateListCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(UpdatePrivateListCommand command)
        {
            var userId = await _dbContext.Users.AsNoTracking().Where(u => u.Login == command.Login).Select(u => u.Id)
                .SingleAsync();
            var list = await _dbContext.BookLists.SingleAsync(
                l => l.OwnerId == userId && l.Type == BookListType.Private);
            list.Update(command);
            await _dbContext.SaveChangesAsync();
        }
    }
}