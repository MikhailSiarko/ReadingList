using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class AddPrivateItemCommandHandler 
        : AddBookItemCommandHandler<AddPrivateItemCommand, PrivateBookListItem, PrivateBookListItemDto>
    {
        public AddPrivateItemCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<int> GetBookListId(AddPrivateItemCommand command)
        {
            return await DbContext.BookLists.Where(BookListFilterExpressions.PrivateBookListForUser(command.UserLogin))
                       .Select(x => (int?)x.Id).SingleOrDefaultAsync() ??
                   throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                   {
                       ["Email"] = command.UserLogin
                   });
        }

        protected override PrivateBookListItem CreateItem(AddPrivateItemCommand command, int listId)
        {
            return new PrivateBookListItem
            {
                Status = BookItemStatus.ToReading,
                LastStatusUpdateDate = DateTime.Now,
                BookListId = listId,
                Title = command.BookInfo.Title,
                Author = command.BookInfo.Author,
                GenreId = command.BookInfo.GenreId
            };
        }

        protected override async Task SaveAsync(PrivateBookListItem item)
        {
            await DbContext.PrivateBookListItems.AddAsync(item);

            await DbContext.SaveChangesAsync();
        }

        protected override PrivateBookListItemDto Convert(PrivateBookListItem item)
        {
            return Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(item);
        }
    }
}