using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReadingList.Write.Infrastructure
{
    public static class DbContextExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> source, IEnumerable<string> navigationPropertyPaths)
            where T : class
        {
            return navigationPropertyPaths.Aggregate(source, (query, path) => query.Include(path));
        }
        
        public static IEnumerable<string> GetIncludePaths<T>(this DbContext context)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            
            return entityType.GetNavigations().Select(n => n.Name).ToList();
        }
    }
}