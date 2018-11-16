using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Write.FetchHandlers
{
    public class GetExistingTagsFetchHandler : IFetchHandler<GetExistingTagsQuery, IEnumerable<Tag>>
    {
        private readonly WriteDbContext _dbContext;

        public GetExistingTagsFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tag>> Fetch(GetExistingTagsQuery query)
        {
            return await _dbContext.Tags.Where(t => query.TagsNames.Contains(t.Name)).ToListAsync();
        }
    }
}