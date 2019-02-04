using MediatR;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Read.Queries
{
    public class GetUser : IRequest<User>
    {
        public readonly int UserId;

        public GetUser(int userId)
        {
            UserId = userId;
        }
    }
}