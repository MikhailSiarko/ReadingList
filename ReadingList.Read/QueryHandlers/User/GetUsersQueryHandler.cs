using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Models.Read;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetUsersQueryHandler : QueryHandler<GetModerators, IEnumerable<ModeratorDto>>
    {
        public GetUsersQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<ModeratorDto>> Handle(SqlQueryContext<GetModerators, IEnumerable<ModeratorDto>> context)
        {
            return await DbConnection.QueryAsync<ModeratorDto>(context.Sql, context.Parameters);
        }
    }
}