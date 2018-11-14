using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Read.Queries
{
    public class GetPrivateListItemQuery : SecuredQuery<PrivateBookListItemDto>
    {
        public readonly int ItemId;

        public GetPrivateListItemQuery(int itemId, int userId) : base(userId)
        {
            ItemId = itemId;
        }
    }
}