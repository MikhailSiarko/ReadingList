using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class AddSharedListItemCommand : AddListItemCommand<SharedBookListItemDto>
    {
        public readonly int ListId;

        public AddSharedListItemCommand(int listId, int userId, BookInfo bookInfo) : base(userId, bookInfo)
        {
            ListId = listId;
        }
    }
}