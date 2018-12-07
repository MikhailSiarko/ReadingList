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
        FindSharedBookListsQueryHandler : QueryHandler<FindSharedLists, IEnumerable<SharedBookListPreviewDto>>
    {
        public FindSharedBookListsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<SharedBookListPreviewDto>> Handle(
            SqlQueryContext<FindSharedLists, IEnumerable<SharedBookListPreviewDto>> context)
        {
            var rows = (await DbConnection.QueryAsync<SharedListDbRow>(context.Sql, context.Parameters)).ToList();

            return rows.Select(r => new SharedBookListPreviewDto()
            {
                Id = r.Id,
                OwnerId = r.OwnerId,
                Name = r.Name,
                Type = r.Type,
                BooksCount = r.BookCount,
                Tags = r.Tags.Split(',').Where(t => !string.IsNullOrEmpty(t)).ToList()
            });
        }

        private class SharedListDbRow
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int OwnerId { get; set; }

            public int Type { get; set; }

            public string Tags { get; set; }

            public int BookCount { get; set; }
        }
    }
}