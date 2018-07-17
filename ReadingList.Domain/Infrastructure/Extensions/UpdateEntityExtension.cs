using System;
using System.Linq;
using System.Reflection;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class UpdateEntityExtension
    {
        public static void Update<TEntity>(this TEntity entity, object source) where TEntity : Entity
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));
            
            var entityProperties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var sourceFields = source.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            if (sourceFields.Any())
            {
                foreach (var sourceField in sourceFields)
                {
                    var entityProperty = entityProperties.SingleOrDefault(p => p.Name == sourceField.Name);
                    if (entityProperty == null ||
                        entityProperty.CustomAttributes.Any(a => a.AttributeType == typeof(IgnoreUpdateAttribute)))
                        continue;
                    entityProperty.SetValue(entity, sourceField.GetValue(source));
                } 
            }
            else
            {
                var sourceProperties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (!sourceProperties.Any())
                    return;
                foreach (var sourceProperty in sourceProperties)
                {
                    var entityProperty = entityProperties.SingleOrDefault(p => p.Name == sourceProperty.Name);
                    if (entityProperty == null ||
                        entityProperty.CustomAttributes.Any(a => a.AttributeType == typeof(IgnoreUpdateAttribute)))
                        continue;
                    entityProperty.SetValue(entity, sourceProperty.GetValue(source));
                }
            }
        }
    }
}