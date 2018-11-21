using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class FindSharedBookListsQueryHandler : QueryHandler<FindSharedListsQuery, IEnumerable<SharedBookListPreviewDto>>
    {
        public FindSharedBookListsQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<SharedBookListPreviewDto>> Handle(SqlQueryContext<FindSharedListsQuery, IEnumerable<SharedBookListPreviewDto>> context)
        {           
            var rows = (await DbConnection.QueryAsync<SharedListDbRow>(context.Sql, context.Parameters)).ToList();

            var lists = new List<SharedBookListPreviewDto>();
            
            foreach (var row in rows)
            {
                if (lists.Any(a => a.Id == row.Id)) 
                    continue;
                
                var tags = rows.Where(r => r.Id == row.Id).Select(r => r.Tag).ToList();

                lists.Add(new SharedBookListPreviewDto()
                {
                    Id = row.Id,
                    OwnerId = row.OwnerId,
                    Name = row.Name,
                    Type = row.Type,
                    BooksCount = row.BookCount,
                    Tags = tags
                });
            }
            
            return lists;
        }
        
        private class SharedListDbRow
        {
            public int Id { get; set; }
            
            public string Name { get; set; }

            public int OwnerId { get; set; }

            public int Type { get; set; }

            public string Tag { get; set; }

            public int BookCount { get; set; }
        }
    }
}
