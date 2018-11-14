namespace ReadingList.Domain.FetchQueries
{
    public class GetSharedListsByUserIdQuery
    {
        public readonly int UserId;

        public GetSharedListsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}