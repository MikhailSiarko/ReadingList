namespace ReadingList.Domain.Commands
{
    public class DeletePrivateItemCommand : SecuredCommand
    {
        public readonly int ItemId;

        public DeletePrivateItemCommand(int itemId, int userId) : base(userId)
        {
            ItemId = itemId;
        }
    }
}