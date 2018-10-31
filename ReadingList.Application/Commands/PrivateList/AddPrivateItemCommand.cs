using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure;

namespace ReadingList.Application.Commands
{
    public class AddPrivateItemCommand : AddListItemCommand<PrivateBookListItemDto>
    {
        public AddPrivateItemCommand(string login, BookInfo bookInfo) : base(login, bookInfo)
        {
        }
    }
}