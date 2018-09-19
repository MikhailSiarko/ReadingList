using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class WriteDbContextExtensions
    {
        public static async Task UpdateOrAddSharedListTags(this WriteDbContext dbContext, IEnumerable<string> tags, BookListWm list)
        {
            await dbContext.Tags.Where(x => tags.Contains(x.Name)).ForEachAsync(x =>
            {
                x.SharedBookListTags.Add(new SharedBookListTagWm
                {
                    Tag = x,
                    SharedBookList = list
                });
            });

            var newTags = tags.Where(x => !dbContext.Tags.Any(y => y.Name == x)).Select(x => new TagWm
            {
                Name = x
            }).ToList();

            foreach (var newTag in newTags)
            {
                newTag.SharedBookListTags = new List<SharedBookListTagWm>(new[]
                {
                    new SharedBookListTagWm
                    {
                        Tag = newTag,
                        SharedBookList = list
                    }
                });
            }

            await dbContext.Tags.AddRangeAsync(newTags);
        }
        
        public static async Task UpdateOrAddSharedListItemTags(this WriteDbContext dbContext, IEnumerable<string> tags, SharedBookListItemWm item)
        {
            await dbContext.Tags.Where(x => tags.Contains(x.Name)).ForEachAsync(x =>
            {
                x.SharedBookListItemTags.Add(new SharedBookListItemTagWm
                {
                    Tag = x,
                    SharedBookListItem = item
                });
            });

            var newTags = tags.Where(x => !dbContext.Tags.Any(y => y.Name == x)).Select(x => new TagWm
            {
                Name = x
            }).ToList();

            foreach (var newTag in newTags)
            {
                newTag.SharedBookListItemTags = new List<SharedBookListItemTagWm>(new[]
                {
                    new SharedBookListItemTagWm
                    {
                        Tag = newTag,
                        SharedBookListItem = item
                    }
                });
            }

            await dbContext.Tags.AddRangeAsync(newTags);
        }
    }
}