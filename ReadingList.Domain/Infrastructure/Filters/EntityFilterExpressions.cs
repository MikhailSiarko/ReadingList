using ReadingList.WriteModel.Models.Base;

namespace ReadingList.Domain.Infrastructure.Filters
{
    public static class EntityFilterExpressions
    {
        public static FilterExpression<TEntity> FindEntity<TEntity>(int id) where TEntity : EntityWm
        {
            return FilterExpression<TEntity>.Create(item => item.Id == id);
        }
    }
}
