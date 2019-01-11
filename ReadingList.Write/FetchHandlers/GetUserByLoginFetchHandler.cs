using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Write.FetchHandlers
{
    public class GetUserByLoginFetchHandler : IFetchHandler<GetUserByLogin, User>
    {
        private readonly WriteDbContext _dbContext;

        public GetUserByLoginFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(GetUserByLogin query)
        {
            return await _dbContext.Table<User>().SingleOrDefaultAsync(u => u.Login == query.Login);
        }
    }
}