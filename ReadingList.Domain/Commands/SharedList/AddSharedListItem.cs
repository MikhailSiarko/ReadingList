using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class AddSharedListItem : AddListItem<SharedBookListItemDto>
    {
        public readonly int ListId;

        public AddSharedListItem(int listId, int userId, int bookId) : base(userId, bookId)
        {
            ListId = listId;
        }
    }
}