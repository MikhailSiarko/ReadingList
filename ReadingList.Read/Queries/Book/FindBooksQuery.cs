using System.Collections.Generic;
using MediatR;
using ReadingList.Domain.Models.DTO.Book;

namespace ReadingList.Read.Queries.Book
{
    public class FindBooksQuery : IRequest<IEnumerable<BookDto>>
    {
        public readonly string Query;

        public FindBooksQuery(string query)
        {
            Query = query;
        }
    }
}