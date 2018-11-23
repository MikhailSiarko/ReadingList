using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddPrivateItemCommandHandler
        : AddBookItemCommandHandler<AddPrivateItemCommand, PrivateBookListItem, PrivateBookListItemDto>
    {
        private readonly IFetchHandler<GetPrivateListByUserIdQuery, BookList> _listFetchHandler;

        public AddPrivateItemCommandHandler(IDataStorage writeService,
            IFetchHandler<GetBookByAuthorAndTitleQuery, Book> bookFetchHandler,
            IFetchHandler<GetBookListItemQuery, PrivateBookListItem> itemFetchHandler,
            IFetchHandler<GetPrivateListByUserIdQuery, BookList> listFetchHandler)
            : base(writeService, bookFetchHandler, itemFetchHandler)
        {
            _listFetchHandler = listFetchHandler;
        }

        protected override async Task<int> GetBookListId(AddPrivateItemCommand command)
        {
            var list = await _listFetchHandler.Fetch(new GetPrivateListByUserIdQuery(command.UserId));

            if (list == null)
            {
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.UserId.ToString()
                });
            }

            return list.Id;
        }

        protected override PrivateBookListItem CreateItem(Book book, int listId)
        {
            return new PrivateBookListItem
            {
                BookListId = listId,
                BookId = book.Id,
                Book = book
            };
        }

        protected override async Task SaveAsync(PrivateBookListItem item)
        {
            await WriteService.SaveAsync(item);
        }

        protected override PrivateBookListItemDto Convert(PrivateBookListItem item)
        {
            return Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(item);
        }
    }
}