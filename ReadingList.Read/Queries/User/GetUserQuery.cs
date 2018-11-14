using MediatR;
using ReadingList.Domain.Models.DAO.Identity;

namespace ReadingList.Read.Queries
{
    public class GetUserQuery : IRequest<User>
    {
        public readonly int UserId;

        public GetUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}