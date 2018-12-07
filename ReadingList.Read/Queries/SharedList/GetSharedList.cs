using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetSharedList : SecuredQuery<SharedBookListDto>
    {
        public readonly int ListId;

        public GetSharedList(int listId, int userId) : base(userId)
        {
            ListId = listId;
        }
    }
}