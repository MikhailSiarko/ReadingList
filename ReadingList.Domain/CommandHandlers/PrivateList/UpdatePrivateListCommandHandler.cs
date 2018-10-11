using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdatePrivateListCommandHandler : UpdateCommandHandler<UpdatePrivateListCommand, BookListWm, PrivateBookListDto>
    {
        public UpdatePrivateListCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService) 
            : base(dbContext, entityUpdateService)
        {
        }

        protected override PrivateBookListDto Convert(BookListWm entity)
        {
            var items = DbContext.PrivateBookListItems.Where(
                    BookListItemFilterExpressions.ItemBelongsToList<PrivateBookListItemWm>(entity.Id))
                .Select(x => Mapper.Map<PrivateBookListItemWm, PrivateBookListItemDto>(x)).ToList();

            return Mapper.Map<BookListWm, PrivateBookListDto>(entity,
                options => options.AfterMap((wm, listDto) => listDto.Items = items));
        }

        protected override void Update(BookListWm entity, UpdatePrivateListCommand command)
        {
            EntityUpdateService.Update(entity, new Dictionary<string, object>
            {
                [nameof(BookListWm.Name)] = command.Name
            });
        }

        protected override async Task<BookListWm> GetEntity(UpdatePrivateListCommand command)
        {
            return await DbContext.BookLists.SingleOrDefaultAsync(BookListFilterExpressions.FindPrivateBookList(command.UserLogin)) ??
                throw new ObjectNotExistForException<BookListWm, UserWm>(null, new OnExceptionObjectDescriptor
                {
                    ["Email"] = command.UserLogin
                });
        }
    }
}