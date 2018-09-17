using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries.SharedList
{
    public class GetSharedListItemsQuery : IQuery<IEnumerable<SharedBookListItemDto>>
    {
        public int ListId { get; set; }
        
        public GetSharedListItemsQuery(int listId)
        {
            ListId = listId;
        }
    }
}