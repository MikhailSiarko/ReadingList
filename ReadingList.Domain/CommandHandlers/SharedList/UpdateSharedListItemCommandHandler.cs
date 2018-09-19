using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class UpdateSharedListItemCommandHandler : CommandHandler<UpdateSharedListItemCommand>
    {
        private readonly WriteDbContext _dbContext;
        private readonly IEntityUpdateService _entityUpdateService;

        public UpdateSharedListItemCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService)
        {
            _dbContext = dbContext;
            _entityUpdateService = entityUpdateService;
        }

        protected override async Task Handle(UpdateSharedListItemCommand command)
        {
            var item = await _dbContext.PrivateBookListItems.FirstOrDefaultAsync(i =>
                           i.BookList.Owner.Login == command.UserLogin && i.Id == command.ItemId &&
                           i.BookListId == command.ListId) ??
                       throw new ObjectNotExistException<SharedBookListItemWm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.ItemId.ToString()
                       });
            
            var tags = await _dbContext.Tags.Where(t => command.Tags.Contains(t.Name)).ToListAsync();
            
            _entityUpdateService.Update(item, new Dictionary<string, object>
            {
                [nameof(SharedBookListItemWm.Title)] = command.BookInfo.Title,
                [nameof(SharedBookListItemWm.Author)] = command.BookInfo.Author,
                [nameof(SharedBookListItemWm.GenreId)] = command.GenreId,
                [nameof(SharedBookListItemWm.SharedBookListItemTags)] = tags.Select(t => new SharedBookListItemTagWm
                {
                    TagId = t.Id,
                    SharedBookListItemId = item.Id
                })
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}