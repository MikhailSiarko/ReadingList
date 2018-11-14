using System.Collections.Generic;
using MediatR;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Read.Queries
{
    public class FindSharedListsQuery : IRequest<IEnumerable<SharedBookListPreviewDto>>
    {
        public readonly string Query;

        public FindSharedListsQuery(string query)
        {
            Query = query;
        }
    }
}
