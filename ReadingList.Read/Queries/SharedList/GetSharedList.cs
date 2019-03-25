using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetSharedList : IRequest<SharedBookListDto>
    {
        public readonly int UserId;

        public readonly int ListId;

        public GetSharedList(int listId, int userId)
        {
            UserId = userId;
            ListId = listId;
        }
    }
}