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

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class UpdateSharedListCommandHandler : UpdateCommandHandler<UpdateSharedListCommand, BookListWm>,
        IValidatable<UpdateSharedListCommand, BookListWm>
    {
        public UpdateSharedListCommandHandler(WriteDbContext dbContext, IEntityUpdateService updateService) 
            : base(dbContext, updateService)
        {
        }

        protected override void Update(BookListWm entity, UpdateSharedListCommand command)
        {
            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(BookListWm.Name)] = command.Name,
                [nameof(BookListWm.SharedBookListTags)] = DbContext.UpdateOrAddSharedListTags(command.Tags, entity).RunSync().ToList()
            });
        }

        public void Validate(BookListWm entity, UpdateSharedListCommand command)
        {
            BookListAccessValidator.Validate(command.UserLogin, entity);
        }

        protected override async Task<BookListWm> GetEntity(UpdateSharedListCommand command)
        {
            var list = await DbContext.BookLists
                    .Include(l => l.SharedBookListTags)
                    .Include(s => s.Owner)
                    .Include(s => s.BookListModerators)
                    .ThenInclude(m => m.User)
                    .SingleAsync(l =>
                        l.Owner.Login == command.UserLogin && l.Id == command.ListId &&
                        l.Type == BookListType.Shared) ??
                throw new ObjectNotExistForException<BookListWm, UserWm>(null, new OnExceptionObjectDescriptor
                {
                    ["Email"] = command.UserLogin
                });
            
            Validate(list, command);

            return list;
        }
    }
}