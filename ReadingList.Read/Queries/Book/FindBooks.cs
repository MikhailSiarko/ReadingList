using System.Collections.Generic;
using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries.Book
{
    public class FindBooks : IRequest<IEnumerable<BookDto>>
    {
        public readonly string Query;

        public FindBooks(string query)
        {
            Query = query;
        }
    }
}