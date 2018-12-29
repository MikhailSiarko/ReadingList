using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Models.Read.Abstractions;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetModeratedListsQueryHandler : QueryHandler<GetModeratedLists, IEnumerable<BookListDto>>
    {
        public GetModeratedListsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<BookListDto>> Handle(
            SqlQueryContext<GetModeratedLists, IEnumerable<BookListDto>> context)
        {
            return await DbConnection.QueryAsync<BookListDto>(context.Sql, context.Parameters);
        }
    }
}