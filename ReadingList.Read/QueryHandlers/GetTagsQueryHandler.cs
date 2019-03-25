using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Models.Read;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetTagsQueryHandler : QueryHandler<GetTags, IEnumerable<TagDto>>
    {
        public GetTagsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<TagDto>> Handle(SqlQueryContext<GetTags, IEnumerable<TagDto>> context)
        {
            return await DbConnection.QueryAsync<TagDto>(context.Sql);
        }
    }
}