using System.Collections.Generic;
using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class FindSharedLists : IRequest<IEnumerable<SharedBookListPreviewDto>>
    {
        public readonly string Query;

        public FindSharedLists(string query)
        {
            Query = query;
        }
    }
}