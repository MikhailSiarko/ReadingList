using System.Collections.Generic;
using ReadingList.Models;
using ReadingList.Models.Write;

namespace ReadingList.Domain.Infrastructure
{
    public static class EntityExtensions
    {
        public static void Update<TEntity>(this TEntity entity, Dictionary<string, object> source)
            where TEntity : Entity
        {
            ObjectUpdater<TEntity>.Update(entity, source);
        }

        public static void Update(this PrivateBookListItem item, Dictionary<string, object> source,
            BookItemStatus newStatus)
        {
            var newReadingTime = ReadingTimeCalculator.Calculate(item.ReadingTimeInSeconds, item.Status,
                item.LastStatusUpdateDate, newStatus);
            source.Add(nameof(PrivateBookListItem.ReadingTimeInSeconds), newReadingTime);
            ObjectUpdater<PrivateBookListItem>.Update(item, source);
        }
    }
}