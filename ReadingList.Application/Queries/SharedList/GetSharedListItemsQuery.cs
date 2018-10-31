using System.Collections.Generic;
using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries
{
    public class GetSharedListItemsQuery : Query<IEnumerable<SharedBookListItemDto>>
    {
        public readonly int ListId;
        
        public GetSharedListItemsQuery(int listId)
        {
            ListId = listId;
        }
    }
}