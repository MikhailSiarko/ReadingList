using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class AddSharedListItemCommand : AddListItemCommand<SharedBookListItemDto>
    {
        public readonly int ListId;

        public AddSharedListItemCommand(int listId, int userId, int bookId) : base(userId, bookId)
        {
            ListId = listId;
        }
    }
}