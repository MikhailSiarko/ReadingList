using MediatR;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Read.Queries
{
    public class GetSharedListItemQuery : IRequest<SharedBookListItemDto>
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