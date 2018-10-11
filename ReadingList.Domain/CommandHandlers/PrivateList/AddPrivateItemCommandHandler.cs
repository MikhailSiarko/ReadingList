using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddPrivateItemCommandHandler 
        : AddBookItemCommandHandler<AddPrivateItemCommand, PrivateBookListItemWm, PrivateBookListItemDto>
    {
        public AddPrivateItemCommandHandler(WriteDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<BookListWm> GetBookList(AddPrivateItemCommand command)
        {
           return await DbContext.BookLists.SingleOrDefaultAsync(BookListFilterExpressions.FindPrivateBookList(command.UserLogin)) ??
                       throw new ObjectNotExistForException<BookListWm, UserWm>(null, new OnExceptionObjectDescriptor
                       {
                           ["Email"] = command.UserLogin
                       });
        }

        protected override PrivateBookListItemWm CreateItem(AddPrivateItemCommand command, BookListWm list)
        {
            return new PrivateBookListItemWm
            {
                Status = BookItemStatus.ToReading,
                LastStatusUpdateDate = DateTime.Now,
                BookListId = list.Id,
                BookList = list,
                Title = command.BookInfo.Title,
                Author = command.BookInfo.Author,
                GenreId = command.BookInfo.GenreId
            };
        }

        protected override async Task SaveAsync(PrivateBookListItemWm item)
        {
            await DbContext.PrivateBookListItems.AddAsync(item);

            await DbContext.SaveChangesAsync();
        }

        protected override PrivateBookListItemDto Convert(PrivateBookListItemWm item)
        {
            return Mapper.Map<PrivateBookListItemWm, PrivateBookListItemDto>(item);
        }
    }
}