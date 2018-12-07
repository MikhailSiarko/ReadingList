namespace ReadingList.Domain.Queries
{
    public class GetPrivateListByUserId
    {
        public readonly int UserId;

        public GetPrivateListByUserId(int userId)
        {
            UserId = userId;
        }
    }
}