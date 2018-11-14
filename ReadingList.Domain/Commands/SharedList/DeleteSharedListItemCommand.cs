namespace ReadingList.Domain.Commands
{
    public class DeleteSharedListItemCommand : SecuredCommand
    {
        public readonly int ItemId;

        public readonly int ListId;
        
        public DeleteSharedListItemCommand(int userId, int listId, int itemId) : base(userId)
        {
            ListId = listId;
            ItemId = itemId;
        }
    }
}