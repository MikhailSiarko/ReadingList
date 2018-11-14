using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetUserSharedListsQueryHandler : QueryHandler<GetUserSharedListsQuery, IEnumerable<SharedBookListPreviewDto>>
    {

        public GetUserSharedListsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<SharedBookListPreviewDto>> Handle(SqlQueryContext<GetUserSharedListsQuery, IEnumerable<SharedBookListPreviewDto>> context)
        {
            using (var reader = await DbConnection.QueryMultipleAsync(context.Sql, context.Parameters))
            {
                var lists = (await reader.ReadAsync<SharedBookListPreviewDto>()).ToList();

                var tags = (await reader.ReadAsync<(string TagName, int ListId)>()).ToLookup(tuple => tuple.ListId);

                foreach (var list in lists)
                {
                    list.Tags = tags[list.Id].Select(t => t.TagName).ToList();
                }

                return lists;
            }
        }
    }
}