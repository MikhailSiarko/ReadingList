using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Services;
using ReadingList.Domain.Services.Validation;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class UpdateSharedListItemCommandHandler : UpdateCommandHandler<UpdateSharedListItemCommand, SharedBookListItemWm>,
        IValidatable<UpdateSharedListItemCommand, SharedBookListItemWm>
    {
        public UpdateSharedListItemCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService) 
            : base(dbContext, entityUpdateService)
        {
        }

        protected override void Update(SharedBookListItemWm entity, UpdateSharedListItemCommand command)
        {
            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(SharedBookListItemWm.Title)] = command.BookInfo.Title,
                [nameof(SharedBookListItemWm.Author)] = command.BookInfo.Author,
                [nameof(SharedBookListItemWm.GenreId)] = command.BookInfo.GenreId,
                [nameof(SharedBookListItemWm.SharedBookListItemTags)] = DbContext.UpdateOrAddSharedListItemTags(command.Tags, entity).RunSync().ToList()
            });
        }

        public void Validate(SharedBookListItemWm entity, UpdateSharedListItemCommand command)
        {
            BookListAccessValidator.Validate(command.UserLogin, entity.BookList);
        }

        protected override async Task<SharedBookListItemWm> GetEntity(UpdateSharedListItemCommand command)
        {
            var item = await DbContext.SharedBookListItems
                    .Include(i => i.SharedBookListItemTags)
                    .Include(s => s.BookList)
                    .ThenInclude(b => b.BookListModerators)
                    .ThenInclude(m => m.User)
                    .Include(s => s.BookList.Owner).FirstOrDefaultAsync(
                        i => i.Id == command.ItemId && i.BookListId == command.ListId) ??
                throw new ObjectNotExistException<SharedBookListItemWm>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ItemId.ToString()
                });
            
            Validate(item, command);

            return item;
        }
    }
}