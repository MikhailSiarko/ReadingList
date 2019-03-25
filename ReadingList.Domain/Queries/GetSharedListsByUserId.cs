namespace ReadingList.Domain.Queries
{
    public class GetSharedListsByUserId
    {
        public readonly int UserId;

        public GetSharedListsByUserId(int userId)
        {
            UserId = userId;
        }
    }
}