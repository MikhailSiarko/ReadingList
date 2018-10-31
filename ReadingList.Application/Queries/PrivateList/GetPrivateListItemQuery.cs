using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries
{
    public class GetPrivateListItemQuery : SecuredQuery<PrivateBookListItemDto>
    {
        public readonly int ItemId;

        public GetPrivateListItemQuery(int itemId, string login) : base(login)
        {
            ItemId = itemId;
        }
    }
}