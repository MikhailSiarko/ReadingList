using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetSharedListItem : IRequest<SharedBookListItemDto>
    {
        public readonly int ListId;

        public readonly int ItemId;

        public GetSharedListItem(int listId, int itemId)
        {
            ListId = listId;
            ItemId = itemId;
        }
    }
}