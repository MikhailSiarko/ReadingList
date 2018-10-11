using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public class AddPrivateItemCommand : AddListItemCommand<PrivateBookListItemDto>
    {
        public AddPrivateItemCommand(string login, BookInfo bookInfo) : base(login, bookInfo)
        {
        }
    }
}