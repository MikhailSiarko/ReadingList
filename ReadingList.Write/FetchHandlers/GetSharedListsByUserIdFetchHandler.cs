using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Write.FetchHandlers
{
    public class GetSharedListsByUserIdFetchHandler : IFetchHandler<GetSharedListsByUserId, IEnumerable<BookList>>
    {
        private readonly WriteDbContext _dbContext;

        public GetSharedListsByUserIdFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookList>> Handle(GetSharedListsByUserId query)
        {
            return await _dbContext
                .Table<BookList>()
                .Where(b => b.OwnerId == query.UserId && b.Type == BookListType.Shared)
                .ToListAsync();
        }
    }
}