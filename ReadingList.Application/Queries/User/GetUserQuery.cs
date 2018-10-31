using ReadingList.Domain.Entities.Identity;

namespace ReadingList.Application.Queries
{
    public class GetUserQuery : Query<User>
    {
        public readonly int UserId;

        public GetUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}