using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class UpdatePrivateListItem : UpdateListItem<PrivateBookListItemDto>
    {
        public readonly int Status;

        public UpdatePrivateListItem(int userId, int itemId, int status) : base(userId, itemId)
        {
            Status = status;
        }
    }
}