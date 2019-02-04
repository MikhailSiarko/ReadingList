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
        GetUserSharedListsQueryHandler : QueryHandler<GetUserSharedLists, ChunkedCollectionDto<SharedBookListPreviewDto>>
    {
        public GetUserSharedListsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<ChunkedCollectionDto<SharedBookListPreviewDto>> Handle(
            SqlQueryContext<GetUserSharedLists, ChunkedCollectionDto<SharedBookListPreviewDto>> context)
        {
            var rows = (await DbConnection.QueryAsync<FindSharedBookListsQueryHandler.SharedListDbRow>(context.Sql, context.Parameters)).ToList();

            if (!rows.Any()) return ChunkedCollectionDto<SharedBookListPreviewDto>.Empty;

            var items = rows.Take(context.Query.Count).Select(r => new SharedBookListPreviewDto
            {
                Id = r.Id,
                OwnerId = r.OwnerId,
                Name = r.Name,
                Type = r.Type,
                BooksCount = r.BookCount,
                Tags = r.Tags?.Split(',').Where(t => !string.IsNullOrEmpty(t)).ToList() ?? new List<string>()
            }).ToList();

            return new ChunkedCollectionDto<SharedBookListPreviewDto>(items, rows.Count > context.Query.Count,
                context.Query.Chunk);
        }
    }
}