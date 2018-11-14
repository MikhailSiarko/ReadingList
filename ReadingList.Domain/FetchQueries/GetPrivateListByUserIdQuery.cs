namespace ReadingList.Domain.FetchQueries
{
    public class GetPrivateListByUserIdQuery
    {
        public readonly int UserId;

        public GetPrivateListByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}