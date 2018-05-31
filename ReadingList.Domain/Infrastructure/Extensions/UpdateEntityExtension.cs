using System.Linq;
using System.Reflection;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class UpdateEntityExtension
    {
        public static void Update<TEntity, TSource>(this TEntity entity, TSource source) where TEntity : Entity
        {
            var entityProperties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var sourceFields = source.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var sourceField in sourceFields)
            {
                var entityProperty = entityProperties.SingleOrDefault(p => p.Name == sourceField.Name);
                if (entityProperty == null ||
                    entityProperty.CustomAttributes.Any(a => a.AttributeType == typeof(IgnoreUpdateAttribute)))
                    continue;
                entityProperty.SetValue(entity, sourceField.GetValue(source));
            }
        }
    }
}