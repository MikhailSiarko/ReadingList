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
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Application.Services;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class UpdatePrivateListCommandHandler : UpdateCommandHandler<UpdatePrivateListCommand, BookList, PrivateBookListDto>
    {
        public UpdatePrivateListCommandHandler(ApplicationDbContext dbContext, IEntityUpdateService entityUpdateService) 
            : base(dbContext, entityUpdateService)
        {
        }

        protected override PrivateBookListDto Convert(BookList entity)
        {
            var items = DbContext.PrivateBookListItems.Where(
                    BookListItemFilterExpressions.BelongsToListWithId<PrivateBookListItem>(entity.Id))
                .Select(x => Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(x)).ToList();

            return Mapper.Map<BookList, PrivateBookListDto>(entity,
                options => options.AfterMap((wm, listDto) => listDto.Items = items));
        }

        protected override void Update(BookList entity, UpdatePrivateListCommand command)
        {
            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name
            });
        }

        protected override async Task<BookList> GetEntity(UpdatePrivateListCommand command)
        {
            return await DbContext.BookLists.SingleOrDefaultAsync(
                       BookListFilterExpressions.PrivateBookListForUser(command.UserLogin)) ??
                   throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                   {
                       ["Email"] = command.UserLogin
                   });
        }
    }
}