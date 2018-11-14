using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class AddPrivateItemCommand : AddListItemCommand<PrivateBookListItemDto>
    {
        public AddPrivateItemCommand(int userId, BookInfo bookInfo) : base(userId, bookInfo)
        {
        }
    }
}