using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Write.Infrastructure;

namespace ReadingList.Write.FetchHandlers
{
    public class GetItemsByListIdFetchHandler<TItem> : IFetchHandler<GetItemsByListIdQuery, IEnumerable<TItem>>
        where TItem : BookListItem
    {
        private readonly WriteDbContext _dbContext;

        public GetItemsByListIdFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TItem>> Fetch(GetItemsByListIdQuery query)
        {
            return await _dbContext.Set<TItem>()
                .Include(_dbContext.GetIncludePaths<TItem>())
                .Where(i => i.BookListId == query.ListId)
                .ToListAsync();
        }
    }
}