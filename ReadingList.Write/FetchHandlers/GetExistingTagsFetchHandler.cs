using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;

namespace ReadingList.Write.FetchHandlers
{
    public class GetExistingTagsFetchHandler : IFetchHandler<GetExistingTags, IEnumerable<Tag>>
    {
        private readonly WriteDbContext _dbContext;

        public GetExistingTagsFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tag>> Handle(GetExistingTags query)
        {
            return await _dbContext.Tags.Where(t => query.TagsNames.Contains(t.Name)).ToListAsync();
        }
    }
}