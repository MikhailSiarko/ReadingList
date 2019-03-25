using System.Collections.Generic;
using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetSharedListItems : IRequest<IEnumerable<SharedBookListItemDto>>
    {
        public readonly int ListId;

        public GetSharedListItems(int listId)
        {
            ListId = listId;
        }
    }
}