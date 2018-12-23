namespace ReadingList.Domain.Queries
{
    public class GetListAccessForUser
    {
        public readonly int UserId;

        public GetListAccessForUser(int userId)
        {
            UserId = userId;
        }
    }
}