using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Services.Validation;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using BookListWm = ReadingList.WriteModel.Models.BookList;

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
            
            EntitiesValidator.Validate(list,
                new OnNotExistExceptionData(typeof(BookListWm).Name, new {email = command.UserLogin}));
            
            list.Update(new {command.Name});
            
            await _dbContext.SaveChangesAsync();
        }
    }
}