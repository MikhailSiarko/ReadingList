namespace ReadingList.Application.Commands
{
    public class DeleteSharedListItemCommand : SecuredCommand
    {
        public readonly int ItemId;

        public readonly int ListId;
        
        public DeleteSharedListItemCommand(string userLogin, int listId, int itemId) : base(userLogin)
        {
            ListId = listId;
            ItemId = itemId;
        }
    }
}