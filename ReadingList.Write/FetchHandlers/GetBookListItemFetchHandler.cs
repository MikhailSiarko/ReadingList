using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Write.Infrastructure;

namespace ReadingList.Write.FetchHandlers
{
    public class GetBookListItemFetchHandler<TItem> : IFetchHandler<GetBookListItemQuery, TItem> where TItem : BookListItem
    {
        private readonly WriteDbContext _dbContext;

        public GetBookListItemFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TItem> Fetch(GetBookListItemQuery query)
        {
            return await _dbContext.Set<TItem>()
                .Include(_dbContext.GetIncludePaths<TItem>())
                .SingleOrDefaultAsync(i =>
                    i.Book.Author == query.Author && i.Book.Title == query.Title && i.BookListId == query.ListId);
        }
    }
}