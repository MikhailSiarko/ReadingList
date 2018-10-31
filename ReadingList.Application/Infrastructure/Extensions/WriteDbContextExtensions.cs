using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Entities.HelpEntities;
using ReadingList.Write;

namespace ReadingList.Application.Infrastructure.Extensions
{
    public static class ApplicationDbContextExtensions
    {
        public static async Task<IEnumerable<SharedBookListTag>> UpdateOrAddSharedListTags(this ApplicationDbContext dbContext,
            IEnumerable<string> tags, BookList list)
        {
            if (list.SharedBookListTags != null)
            {
                dbContext.SharedBookListTags.RemoveRange(list.SharedBookListTags);
            
                await dbContext.SaveChangesAsync();
            }

            var existingTags = await dbContext.Tags.Where(x => tags.Contains(x.Name)).ToListAsync();

            var newTags = tags.Where(x => !dbContext.Tags.Any(y => y.Name == x)).Select(x => new Tag
            {
                Name = x
            }).ToList();

            await dbContext.Tags.AddRangeAsync(newTags);
            
            return existingTags.Concat(newTags).Select(t => new SharedBookListTag
            {
                TagId = t.Id,
                SharedBookListId = list.Id
            });
        }
        
        public static async Task<IEnumerable<SharedBookListItemTag>> UpdateOrAddSharedListItemTags(this ApplicationDbContext dbContext,
            IEnumerable<string> tags, SharedBookListItem item)
        {
            if (item.SharedBookListItemTags != null)
            {
                dbContext.SharedBookListItemTags.RemoveRange(item.SharedBookListItemTags);

                await dbContext.SaveChangesAsync();
            }
            
            var existingTags = await dbContext.Tags.Where(x => tags.Contains(x.Name)).ToListAsync();

            var newTags = tags.Where(x => !dbContext.Tags.Any(y => y.Name == x)).Select(x => new Tag
            {
                Name = x
            }).ToList();

            await dbContext.Tags.AddRangeAsync(newTags);

            return existingTags.Concat(newTags).Select(t => new SharedBookListItemTag
            {
                TagId = t.Id,
                SharedBookListItemId = item.Id
            });
        }
    }
}