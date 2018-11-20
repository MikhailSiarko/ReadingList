using System.Collections.Generic;
using System.Data;
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

        protected override async Task<IEnumerable<BookDto>> Handle(SqlQueryContext<FindBooksQuery, IEnumerable<BookDto>> context)
        {
            return await DbConnection.QueryAsync<BookDto>(context.Sql, context.Parameters);
        }
    }
}