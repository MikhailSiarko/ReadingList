using System.Collections.Generic;
using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries.SharedList
{
    public class FindSharedListsQuery : Query<IEnumerable<SharedBookListPreviewDto>>
    {
        public string Query { get; set; }

        public FindSharedListsQuery(string query)
        {
            Query = query;
        }
    }
}
