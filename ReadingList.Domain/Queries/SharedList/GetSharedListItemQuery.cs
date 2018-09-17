using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Queries.SharedList
{
    public class GetSharedListItemQuery : IQuery<SharedBookListItemDto>
    {
        public readonly int ListId;

        public readonly int ItemId;

        public GetSharedListItemQuery(int listId, int itemId)
        {
            ListId = listId;
            ItemId = itemId;
        }
    }
}