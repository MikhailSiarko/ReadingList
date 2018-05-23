using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Queries
{
    public class GetUserQuery : IQuery<User>
    {
        public readonly string UserId;

        public GetUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}