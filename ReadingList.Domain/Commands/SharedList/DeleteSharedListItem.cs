namespace ReadingList.Domain.Commands
{
    public class DeleteSharedListItem : SecuredCommand
    {
        public readonly int ItemId;

        public readonly int ListId;

        public DeleteSharedListItem(int userId, int listId, int itemId) : base(userId)
        {
            ListId = listId;
            ItemId = itemId;
        }
    }
}