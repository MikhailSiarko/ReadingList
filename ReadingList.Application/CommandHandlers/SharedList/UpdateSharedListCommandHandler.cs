using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Application.Infrastructure.Filters.ValidationFilters;
using ReadingList.Domain.Infrastructure;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class UpdateSharedListCommandHandler : UpdateCommandHandler<UpdateSharedListCommand, BookList, SharedBookListPreviewDto>
    {
        public UpdateSharedListCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override SharedBookListPreviewDto Convert(BookList entity)
        {
            return Mapper.Map<BookList, SharedBookListPreviewDto>(entity);
        }

        protected override void Update(BookList entity, UpdateSharedListCommand command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name,
                [nameof(BookList.SharedBookListTags)] = DbContext.UpdateOrAddSharedListTags(command.Tags, entity).RunSync().ToList()
            });
        }

        protected override async Task<BookList> GetEntity(UpdateSharedListCommand command)
        {
            var listQuery = DbContext.BookLists.Where(BookListFilterExpressions.SharedBookListWithId(command.ListId));

            var list =
                await listQuery.Include(l => l.SharedBookListTags).ThenInclude(t => t.Tag).SingleOrDefaultAsync() ??
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
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