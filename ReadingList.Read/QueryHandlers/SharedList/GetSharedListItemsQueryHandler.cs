using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Models.Read;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class
        GetSharedListItemsQueryHandler : QueryHandler<GetSharedListItems, IEnumerable<SharedBookListItemDto>>
    {
        public GetSharedListItemsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<SharedBookListItemDto>> Handle(
            SqlQueryContext<GetSharedListItems, IEnumerable<SharedBookListItemDto>> context)
        {
            using (var reader = await DbConnection.QueryMultipleAsync(context.Sql, context.Parameters))
            {
                var items = (await reader.ReadAsync<SharedBookListItemDto>()).ToList();

                var tags = (await reader.ReadAsync<(string TagName, int BookId)>()).ToLookup(t => t.BookId);

                foreach (var item in items)
                {
                    item.Tags = tags[item.BookId].Select(t => t.TagName).ToList();
                }

                return items;
            }
        }
    }
}