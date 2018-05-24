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
            InitializeRoles(context);
            context.SaveChanges();
        }

        private static void InitializeRoles(ReadingListDbContext context)
        {
            var roles = ApplicationRoles.GetRoles();
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(roles);
            }
            else
            {
                var unregisteredRoles = roles.Where(s => !context.Roles.Any(i => i.Id == s.Id && i.Name == s.Name));
                context.AddRange(unregisteredRoles);
            }
        }
    }
}
