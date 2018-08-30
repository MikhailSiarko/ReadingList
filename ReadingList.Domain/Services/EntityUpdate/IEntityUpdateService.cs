using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.Domain.Services
{
    public interface IEntityUpdateService
    {
        void Update<TEntity>(TEntity entity, Dictionary<string, object> source) where  TEntity : EntityWm;
    }
}