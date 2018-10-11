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
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddSharedItemCommandHandler 
        : AddBookItemCommandHandler<AddSharedListItemCommand, SharedBookListItemWm, SharedBookListItemDto>
    {
        public AddSharedItemCommandHandler(WriteDbContext dbContext) 
            : base(dbContext)
        {
        }

        protected override async Task<int> GetBookListId(AddSharedListItemCommand command)
        {
            var listQuery = DbContext.BookLists.Where(BookListFilterExpressions.FindSharedBookList(command.ListId));

            if (!await listQuery.AnyAsync())
            {
                throw new ObjectNotExistException<BookListWm>(
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

        protected override SharedBookListItemWm CreateItem(AddSharedListItemCommand command, int listId)
        {           
            var item = new SharedBookListItemWm
            {
                Author = command.BookInfo.Author,
                Title = command.BookInfo.Title,
                BookListId = listId,
                GenreId = command.BookInfo.GenreId
            };

            item.SharedBookListItemTags = DbContext.UpdateOrAddSharedListItemTags(command.Tags, item).RunSync().ToList();
            
            return item;
        }

        protected override async Task SaveAsync(SharedBookListItemWm item)
        {
            await DbContext.SharedBookListItems.AddAsync(item);

            await DbContext.SaveChangesAsync();
        }

        protected override SharedBookListItemDto Convert(SharedBookListItemWm item)
        {
            return Mapper.Map<SharedBookListItemWm, SharedBookListItemDto>(item);
        }
    }
}