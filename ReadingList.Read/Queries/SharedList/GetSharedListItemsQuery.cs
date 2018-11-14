using System.Collections.Generic;
using MediatR;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Read.Queries
{
    public class GetSharedListItemsQuery : IRequest<IEnumerable<SharedBookListItemDto>>
    {
        public readonly int ListId;
        
        public GetSharedListItemsQuery(int listId)
        {
            ListId = listId;
        }
    }
}