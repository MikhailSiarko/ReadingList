using System.Collections.Generic;
using ReadingList.Domain.Entities.Base;

namespace ReadingList.Domain.Infrastructure
{
    public static class Extension
    {
        public static void Update<TEntity>(this TEntity entity, Dictionary<string, object> source) where TEntity : Entity
        {
            ObjectUpdater.Update(entity, source);
        }
    }
}