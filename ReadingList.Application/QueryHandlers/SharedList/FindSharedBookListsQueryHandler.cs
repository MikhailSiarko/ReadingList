using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Queries.SharedList;
using ReadingList.Read;

namespace ReadingList.Application.QueryHandlers
{
    public class FindSharedBookListsQueryHandler : QueryHandler<FindSharedListsQuery, IEnumerable<SharedBookListPreviewDto>>
    {
        public FindSharedBookListsQueryHandler(IApplicationDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<SharedBookListPreviewDto>> Handle(FindSharedListsQuery query)
        {
            return
                await DbConnection.QueryAsync(query.SqlQueryContext,
                    async reader =>
                    {
                        var lists = (await reader.ReadAsync<SharedBookListPreviewDto>()).ToList();

                        var tags = (await reader.ReadAsync<(string TagName, int ListId)>()).ToList();

                        foreach (var listRm in lists)
                        {
                            listRm.Tags = tags.Where(t => t.ListId == listRm.Id).Select(t => t.TagName);
                        }

                        return lists;
                    }) ??
                new List<SharedBookListPreviewDto>();
        }
    }
}
