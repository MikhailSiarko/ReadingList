using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.WriteModel.Models;

namespace ReadingList.WriteModel
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ReadingListDbContext>();
            context.Database.EnsureCreated();
            var statuses = new BookItemStatuses();

            if (!context.BookItemStatuses.Any())
            {
                context.AddRange(statuses);
            }  
            else
            {
                var unregisteredStatuses = statuses.Where(s => !context.BookItemStatuses.Any(i => i.Id == s.Id)).ToList();
                context.AddRange(unregisteredStatuses);
            }

            context.SaveChanges();
        }
    }
}
