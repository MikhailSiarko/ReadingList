namespace ReadingList.Domain.Queries
{
    public class GetListAccessForUser
    {
        public readonly int UserId;

        public readonly int ListId;

        public GetListAccessForUser(int userId, int listId)
        {
            UserId = userId;
            ListId = listId;
        }
    }
}