namespace ReadingList.Domain.Commands
{
    public class DeletePrivateItem : SecuredCommand
    {
        public readonly int ItemId;

        public DeletePrivateItem(int itemId, int userId) : base(userId)
        {
            ItemId = itemId;
        }
    }
}