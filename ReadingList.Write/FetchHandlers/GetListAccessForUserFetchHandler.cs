using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Write.FetchHandlers
{
    public class GetListAccessForUserFetchHandler : IFetchHandler<GetListAccessForUser, (bool editable, bool canBeModerated)>
    {
        private readonly WriteDbContext _context;

        public GetListAccessForUserFetchHandler(WriteDbContext context)
        {
            _context = context;
        }

        public async Task<(bool editable, bool canBeModerated)> Handle(GetListAccessForUser query)
        {
            var access = await _context.BookLists
                .Where(b => b.Id == query.ListId)
                .Select(b => new
                {
                    Editable = b.OwnerId == query.UserId,
                    Moderated = b.OwnerId == query.UserId || b.BookListModerators.Any(m => m.UserId == query.UserId)
                })
                .SingleAsync();

            return (editable: access.Editable, canBeModerated: access.Moderated);
        }
    }
}