using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Write.FetchHandlers
{
    public class GetItemsByBookAndListIdsFetchHandler : IFetchHandler<GetItemsByBookAndListIds, IEnumerable<BookListItem>>
    {
        private readonly WriteDbContext _dbContext;

        public GetItemsByBookAndListIdsFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookListItem>> Handle(GetItemsByBookAndListIds query)
        {
            var items = new List<BookListItem>();

            var privateItems = await _dbContext.PrivateBookListItems
                .Where(i => i.BookId == query.BookId && query.ListsIds.Any(o => o == i.BookListId)).ToListAsync();

            var sharedItems = await _dbContext.SharedBookListItems
                .Where(i => i.BookId == query.BookId && query.ListsIds.Any(o => o == i.BookListId)).ToListAsync();

            items.AddRange(privateItems);
            items.AddRange(sharedItems);

            return items;
        }
    }
}