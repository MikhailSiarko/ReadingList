using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Models.DTO.Book;
using ReadingList.Read.Queries.Book;

namespace ReadingList.Read.QueryHandlers.Book
{
    public class FindBooksQueryHandler : QueryHandler<FindBooksQuery, IEnumerable<BookDto>>
    {
        public FindBooksQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<BookDto>> Handle(
            SqlQueryContext<FindBooksQuery, IEnumerable<BookDto>> context)
        {
            var rows = (await DbConnection.QueryAsync<BookDbRow>(context.Sql, context.Parameters)).ToList();

            var books = new List<BookDto>();

            foreach (var row in rows)
            {
                if (books.Any(a => a.Id == row.Id))
                    continue;

                var tags = rows.Where(b => b.Id == row.Id && !string.IsNullOrEmpty(b.Tag)).Select(b => b.Tag).ToList();

                books.Add(new BookDto
                {
                    Id = row.Id,
                    Author = row.Author,
                    Title = row.Title,
                    Tags = tags
                });
            }

            return books;
        }

        private class BookDbRow
        {
            public int Id { get; set; }

            public string Author { get; set; }

            public string Title { get; set; }

            public string Tag { get; set; }
        }
    }
}