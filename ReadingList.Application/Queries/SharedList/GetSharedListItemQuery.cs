using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries
{
    public class GetSharedListItemQuery : Query<SharedBookListItemDto>
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