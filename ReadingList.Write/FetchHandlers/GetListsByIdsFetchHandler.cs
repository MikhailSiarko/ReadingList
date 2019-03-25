using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Write.FetchHandlers
{
    public class GetListsByIdsFetchHandler : IFetchHandler<GetListsByIds, IEnumerable<BookList>>
    {
        private readonly WriteDbContext _writeDbContext;

        public GetListsByIdsFetchHandler(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task<IEnumerable<BookList>> Handle(GetListsByIds query)
        {
            return await _writeDbContext.BookLists.Where(b => query.ListsIds.Any(i => i == b.Id)).ToListAsync();
        }
    }
}