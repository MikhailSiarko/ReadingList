using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using BookListWm = ReadingList.WriteModel.Models.BookList;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class UpdatePrivateListCommandHandler : CommandHandler<UpdatePrivateListCommand>
    {
        private readonly WriteDbContext _dbContext;
        private readonly IEntityUpdateService _entityUpdateService;

        public UpdatePrivateListCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService)
        {
            _dbContext = dbContext;
            _entityUpdateService = entityUpdateService;
        }

        protected override async Task Handle(UpdatePrivateListCommand command)
        {
            var list = await _dbContext.BookLists.SingleAsync(
                           l => l.Owner.Login == command.UserLogin && l.Type == BookListType.Private) ??
                       throw new ObjectNotExistException<BookListWm>(new {email = command.UserLogin});
            
            _entityUpdateService.Update(list, new Dictionary<string, object>
            {
                [nameof(BookListWm.Name)] = command.Name
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}