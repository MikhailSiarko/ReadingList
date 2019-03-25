using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Write.FetchHandlers
{
    public class GetItemsByListIdFetchHandler<TItem> : IFetchHandler<GetItemsByListId, IEnumerable<TItem>>
        where TItem : BookListItem
    {
        private readonly WriteDbContext _dbContext;

        public GetItemsByListIdFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TItem>> Handle(GetItemsByListId query)
        {
            return await _dbContext
                .Table<TItem>()
                .Where(i => i.BookListId == query.ListId)
                .ToListAsync();
        }
    }
}