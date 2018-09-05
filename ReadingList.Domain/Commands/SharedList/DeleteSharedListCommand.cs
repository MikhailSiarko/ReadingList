namespace ReadingList.Domain.Commands.SharedList
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