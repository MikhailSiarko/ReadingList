namespace ReadingList.Domain.Commands
{
    public class DeleteSharedListCommand : SecuredCommand
    {
        public readonly int ListId;
        
        public DeleteSharedListCommand(string userLogin, int listId) : base(userLogin)
        {
            ListId = listId;
        }
    }
}