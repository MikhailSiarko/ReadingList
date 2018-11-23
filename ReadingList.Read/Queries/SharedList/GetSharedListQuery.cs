using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Read.Queries
{
    public class GetSharedListQuery : SecuredQuery<SharedBookListDto>
    {
        public readonly int ListId;

        public GetSharedListQuery(int listId, int userId) : base(userId)
        {
            ListId = listId;
        }
    }
}