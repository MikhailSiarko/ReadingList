using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Infrastructure;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetTagsQueryHandler : QueryHandler<GetTags, IEnumerable<SelectListItem>>
    {
        public GetTagsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<SelectListItem>> Handle(SqlQueryContext<GetTags, IEnumerable<SelectListItem>> context)
        {
            return await DbConnection.QueryAsync<SelectListItem>(context.Sql);
        }
    }
}