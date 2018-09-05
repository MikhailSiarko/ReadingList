using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Queries
{
    public class GetUserQuery : IQuery<UserRm>
    {
        public readonly int UserId;

        public GetUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}