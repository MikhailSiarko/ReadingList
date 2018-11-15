using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Write.FetchHandlers
{
    public class GetUserByLoginFetchHandler : IFetchHandler<GetUserByLoginQuery, User>
    {
        private readonly WriteDbContext _dbContext;

        public GetUserByLoginFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Fetch(GetUserByLoginQuery query)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == query.Login);
        }
    }
}