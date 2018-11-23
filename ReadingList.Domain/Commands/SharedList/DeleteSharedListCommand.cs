namespace ReadingList.Domain.Commands
{
    public class DeleteSharedListCommand : SecuredCommand
    {
        public readonly int ListId;

        public DeleteSharedListCommand(int userId, int listId) : base(userId)
        {
            ListId = listId;
        }
    }
}