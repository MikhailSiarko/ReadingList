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
    public class UpdateSharedListCommandHandler : CommandHandler<UpdateSharedListCommand>
    {
        private readonly WriteDbContext _dbContext;
        private readonly IEntityUpdateService _updateService;

        public UpdateSharedListCommandHandler(WriteDbContext dbContext, IEntityUpdateService updateService)
        {
            _dbContext = dbContext;
            _updateService = updateService;
        }

        protected override async Task Handle(UpdateSharedListCommand command)
        {
            var list = await _dbContext.BookLists.SingleAsync(l => l.Id == command.ListId && l.Type == BookListType.Shared) ??
                       throw new ObjectNotExistForException<BookListWm, UserWm>(null, new OnExceptionObjectDescriptor
                       {
                           ["Email"] = command.UserLogin
                       });

            var tags = await _dbContext.Tags.Where(t => command.Tags.Contains(t.Name)).ToListAsync();
            
            _updateService.Update(list, new Dictionary<string, object>
            {
                [nameof(BookListWm.Name)] = command.Name,
                [nameof(BookListWm.SharedBookListTags)] = tags.Select(t => new SharedBookListTagWm
                {
                    TagId = t.Id,
                    SharedBookListId = list.Id
                })
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}