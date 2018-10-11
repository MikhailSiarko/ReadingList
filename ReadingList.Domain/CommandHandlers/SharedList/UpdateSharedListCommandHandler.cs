using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.Domain.Infrastructure.Filters.ValidationFilters;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdateSharedListCommandHandler : UpdateCommandHandler<UpdateSharedListCommand, BookListWm, SharedBookListDto>
    {
        public UpdateSharedListCommandHandler(WriteDbContext dbContext, IEntityUpdateService updateService) 
            : base(dbContext, updateService)
        {
        }

        protected override SharedBookListDto Convert(BookListWm entity)
        {
            return Mapper.Map<BookListWm, SharedBookListDto>(entity);
        }

        protected override void Update(BookListWm entity, UpdateSharedListCommand command)
        {
            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(BookListWm.Name)] = command.Name,
                [nameof(BookListWm.SharedBookListTags)] = DbContext.UpdateOrAddSharedListTags(command.Tags, entity).RunSync().ToList()
            });
        }

        protected override async Task<BookListWm> GetEntity(UpdateSharedListCommand command)
        {
            var listQuery = DbContext.BookLists.Where(BookListFilterExpressions.FindSharedBookList(command.ListId));

            var list =
                await listQuery.Include(l => l.SharedBookListTags).ThenInclude(t => t.Tag).SingleOrDefaultAsync() ??
                throw new ObjectNotExistForException<BookListWm, UserWm>(null, new OnExceptionObjectDescriptor
                {
                    ["Email"] = command.UserLogin
                });

            if (!await listQuery.AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOrModerator(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }        

            return list;
        }
    }
}