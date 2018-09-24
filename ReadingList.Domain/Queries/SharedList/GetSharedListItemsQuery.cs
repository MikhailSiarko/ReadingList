using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries
{
    public class GetSharedListItemsQuery : IQuery<IEnumerable<SharedBookListItemDto>>
    {
        public readonly int ListId;
        
        public GetSharedListItemsQuery(int listId)
        {
            ListId = listId;
        }
    }
}