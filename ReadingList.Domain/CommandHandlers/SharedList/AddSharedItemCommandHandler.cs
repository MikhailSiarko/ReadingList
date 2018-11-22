﻿using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddSharedItemCommandHandler 
        : AddBookItemCommandHandler<AddSharedListItemCommand, SharedBookListItem, SharedBookListItemDto>
    {     
        public AddSharedItemCommandHandler(IDataStorage writeService,
            IFetchHandler<GetBookByAuthorAndTitleQuery, Book> bookFetchHandler,
            IFetchHandler<GetBookListItemQuery, SharedBookListItem> itemFetchHandler) 
            : base(writeService, bookFetchHandler, itemFetchHandler)
        {
        }

        protected override async Task<int> GetBookListId(AddSharedListItemCommand command)
        {
            var list = await WriteService.GetAsync<BookList>(command.ListId);
            
            if (list == null)
            {
                throw new ObjectNotExistException<BookList>(
                    new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ListId.ToString()
                    });
            }
            
            var accessSpecification = new BookListAccessSpecification(list);
            
            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }

            return command.ListId;
        }

        protected override SharedBookListItem CreateItem(Book book, int listId)
        {
            var item = new SharedBookListItem
            {
                BookListId = listId,
                BookId = book.Id,
                Book = book
            };
            
            return item;
        }

        protected override async Task SaveAsync(SharedBookListItem item)
        {
            await WriteService.SaveAsync(item);
        }

        protected override SharedBookListItemDto Convert(SharedBookListItem item)
        {
            return Mapper.Map<SharedBookListItem, SharedBookListItemDto>(item);
        }
    }
}