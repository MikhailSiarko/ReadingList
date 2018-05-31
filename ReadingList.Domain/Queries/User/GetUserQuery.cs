using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.Queries
{
    public class GetUserQuery : IQuery<UserRm>
    {
        public readonly string UserId;

        public GetUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}