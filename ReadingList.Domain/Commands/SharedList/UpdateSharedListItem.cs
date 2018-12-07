using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedListItem : UpdateListItem<SharedBookListItemDto>
    {
        public readonly int ListId;

        public UpdateSharedListItem(int userId, int itemId, int listId)
            : base(userId, itemId)
        {
            ListId = listId;
        }
    }
}