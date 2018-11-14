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
    public class GetSharedListsByUserIdFetchHandler : IFetchHandler<GetSharedListsByUserIdQuery, IEnumerable<BookList>>
    {
        private readonly WriteDbContext _dbContext;

        public GetSharedListsByUserIdFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookList>> Fetch(GetSharedListsByUserIdQuery query)
        {
            return await _dbContext.BookLists
                .Include(_dbContext.GetIncludePaths<BookList>())
                .Where(b => b.OwnerId == query.UserId && b.Type == BookListType.Shared)
                .ToListAsync();
        }
    }
}