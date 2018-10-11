using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Queries
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