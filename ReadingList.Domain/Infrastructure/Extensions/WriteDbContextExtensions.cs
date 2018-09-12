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
        public static async Task UpdateOrAddSharedListTags(this WriteDbContext dbContext, string[] tags, BookListWm list)
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

        public static async Task AddCategory(this WriteDbContext dbContext, string category)
        {
            await dbContext.Categories.AddAsync(new CategoryWm
            {
                Name = category
            });
        }
    }
}