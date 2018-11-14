using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Write.Infrastructure;

namespace ReadingList.Write.FetchHandlers
{
    public class GetPrivateListByUserIdFetchHandler : IFetchHandler<GetPrivateListByUserIdQuery, BookList>
    {
        private readonly WriteDbContext _dbContext;

        public GetPrivateListByUserIdFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookList> Fetch(GetPrivateListByUserIdQuery query)
        {
            return await _dbContext.BookLists
                .Include(_dbContext.GetIncludePaths<BookList>())
                .SingleOrDefaultAsync(b => b.OwnerId == query.UserId && b.Type == BookListType.Private);
        }
    }
}