using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetPrivateListItem : SecuredQuery<PrivateBookListItemDto>
    {
        public readonly int ItemId;

        public GetPrivateListItem(int itemId, int userId) : base(userId)
        {
            ItemId = itemId;
        }
    }
}