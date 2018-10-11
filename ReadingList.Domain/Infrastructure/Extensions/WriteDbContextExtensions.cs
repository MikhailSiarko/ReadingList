using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Infrastructure;
using ReadingList.WriteModel.Models;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class WriteDbContextExtensions
    {
        public static async Task<IEnumerable<SharedBookListTagWm>> UpdateOrAddSharedListTags(this WriteDbContext dbContext,
            IEnumerable<string> tags, BookListWm list)
        {
            dbContext.SharedBookListTags.RemoveRange(list.SharedBookListTags);
            
            await dbContext.SaveChangesAsync();
            
            var existingTags = await dbContext.Tags.Where(x => tags.Contains(x.Name)).ToListAsync();

            var newTags = tags.Where(x => !dbContext.Tags.Any(y => y.Name == x)).Select(x => new TagWm
            {
                Name = x
            }).ToList();

            await dbContext.Tags.AddRangeAsync(newTags);
            
            return existingTags.Concat(newTags).Select(t => new SharedBookListTagWm
            {
                TagId = t.Id,
                SharedBookListId = list.Id
            });
        }
        
        public static async Task<IEnumerable<SharedBookListItemTagWm>> UpdateOrAddSharedListItemTags(this WriteDbContext dbContext,
            IEnumerable<string> tags, SharedBookListItemWm item)
        {          
            dbContext.SharedBookListItemTags.RemoveRange(item.SharedBookListItemTags);

            await dbContext.SaveChangesAsync();
            
            var existingTags = await dbContext.Tags.Where(x => tags.Contains(x.Name)).ToListAsync();

            var newTags = tags.Where(x => !dbContext.Tags.Any(y => y.Name == x)).Select(x => new TagWm
            {
                Name = x
            }).ToList();

            await dbContext.Tags.AddRangeAsync(newTags);

            return existingTags.Concat(newTags).Select(t => new SharedBookListItemTagWm
            {
                TagId = t.Id,
                SharedBookListItemId = item.Id
            });
        }
    }
}