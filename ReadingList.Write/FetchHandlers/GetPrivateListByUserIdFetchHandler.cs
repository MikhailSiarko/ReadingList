using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Write.FetchHandlers
{
    public class GetPrivateListByUserIdFetchHandler : IFetchHandler<GetPrivateListByUserId, BookList>
    {
        private readonly WriteDbContext _dbContext;

        public GetPrivateListByUserIdFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookList> Handle(GetPrivateListByUserId query)
        {
            return await _dbContext
                .Table<BookList>()
                .SingleOrDefaultAsync(b => b.OwnerId == query.UserId && b.Type == BookListType.Private);
        }
    }
}