using System.Collections.Generic;
using ReadingList.Domain.Entities.Base;

namespace ReadingList.Application.Services
{
    public interface IEntityUpdateService
    {
        void Update<TEntity>(TEntity entity, Dictionary<string, object> source) where  TEntity : Entity;
    }
}