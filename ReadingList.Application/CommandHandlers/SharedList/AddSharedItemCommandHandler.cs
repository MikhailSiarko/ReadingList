using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Application.Infrastructure.Filters.ValidationFilters;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class AddSharedItemCommandHandler 
        : AddBookItemCommandHandler<AddSharedListItemCommand, SharedBookListItem, SharedBookListItemDto>
    {
        public AddSharedItemCommandHandler(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
        }

        protected override async Task<int> GetBookListId(AddSharedListItemCommand command)
        {
            var listQuery = DbContext.BookLists.Where(BookListFilterExpressions.SharedBookListWithId(command.ListId));

            if (!await listQuery.AnyAsync())
            {
                throw new ObjectNotExistException<BookList>(
                    new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ListId.ToString()
                    });
            }

            if (!await listQuery.AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOrModerator(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            return command.ListId;
        }

        protected override SharedBookListItem CreateItem(AddSharedListItemCommand command, int listId)
        {           
            var item = new SharedBookListItem
            {
                Author = command.BookInfo.Author,
                Title = command.BookInfo.Title,
                BookListId = listId,
                GenreId = command.BookInfo.GenreId
            };

            item.SharedBookListItemTags = DbContext.UpdateOrAddSharedListItemTags(command.Tags, item).RunSync().ToList();
            
            return item;
        }

        protected override async Task SaveAsync(SharedBookListItem item)
        {
            await DbContext.SharedBookListItems.AddAsync(item);

            await DbContext.SaveChangesAsync();
        }

        protected override SharedBookListItemDto Convert(SharedBookListItem item)
        {
            return Mapper.Map<SharedBookListItem, SharedBookListItemDto>(item);
        }
    }
}