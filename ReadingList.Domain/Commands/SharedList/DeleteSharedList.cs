namespace ReadingList.Domain.Commands
{
    public class DeleteSharedList : SecuredCommand
    {
        public readonly int ListId;

        public DeleteSharedList(int userId, int listId) : base(userId)
        {
            ListId = listId;
        }
    }
}