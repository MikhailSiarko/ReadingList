using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Write.FetchHandlers
{
    public class GetBookListItemFetchHandler<TItem> : IFetchHandler<GetBookListItem, TItem>
        where TItem : BookListItem
    {
        private readonly WriteDbContext _dbContext;

        public GetBookListItemFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TItem> Handle(GetBookListItem query)
        {
            return await _dbContext
                .Table<TItem>()
                .SingleOrDefaultAsync(i => i.BookId == query.BookId && i.BookListId == query.ListId);
        }
    }
}