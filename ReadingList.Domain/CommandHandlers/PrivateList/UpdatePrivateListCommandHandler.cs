using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
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
            var list = await _dbContext.BookLists.SingleAsync(
                l => l.Owner.Login == command.UserLogin && l.Type == BookListType.Private);
            
            if(list == null)
                throw new ObjectNotExistException($"Private list for user {command.UserLogin}");
            
            list.Update(new {command.Name});
            
            await _dbContext.SaveChangesAsync();
        }
    }
}