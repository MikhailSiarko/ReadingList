using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddPrivateItemCommandHandler
        : AddBookItemCommandHandler<AddPrivateItem, PrivateBookListItem, PrivateBookListItemDto>
    {
        private readonly IFetchHandler<GetPrivateListByUserId, BookList> _listFetchHandler;

        public AddPrivateItemCommandHandler(IDataStorage writeService,
            IFetchHandler<GetBookListItem, PrivateBookListItem> itemFetchHandler,
            IFetchHandler<GetPrivateListByUserId, BookList> listFetchHandler)
            : base(writeService, itemFetchHandler)
        {
            _listFetchHandler = listFetchHandler;
        }

        protected override async Task<int> GetBookListId(AddPrivateItem command)
        {
            var list = await _listFetchHandler.Handle(new GetPrivateListByUserId(command.UserId));

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