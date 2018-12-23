using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Write.FetchHandlers
{
    public class GetListAccessForUserFetchHandler : IFetchHandler<GetListAccessForUser, bool>
    {
        private readonly WriteDbContext _context;

        public GetListAccessForUserFetchHandler(WriteDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(GetListAccessForUser query)
        {
            return await _context.BookLists.AnyAsync(b =>
                b.OwnerId == query.UserId || b.BookListModerators.Any(m => m.UserId == query.UserId));
        }
    }
}