using ReadingList.Domain.Entities.Base;

namespace ReadingList.Application.Infrastructure.Filters
{
    public static class EntityFilterExpressions
    {
        public static FilterExpression<TEntity> WithId<TEntity>(int id) where TEntity : Entity
        {
            return FilterExpression<TEntity>.Create(item => item.Id == id);
        }
    }
}
