using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public static void UpdateExpr<TEntity>(this TEntity entity, Dictionary<string, object> source) where TEntity : Entity
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                return;
            
            var entityProperties = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var properties = Expression.Constant(entityProperties);
            
            var i = Expression.Parameter(typeof(int), "i");
            var j = Expression.Parameter(typeof(int), "j");

            var propertiesArrayLength = Expression.Parameter(typeof(int), "propertiesArrayLength");
            var customAttributesLength = Expression.Parameter(typeof(int), "customAttributesLength");

            var property = Expression.Parameter(typeof(PropertyInfo), "property");
            var value = Expression.Parameter(typeof(object), "value");
            var endOuterLoop = Expression.Label("endOuterLoop");
            var endInnerLoop = Expression.Label("endInnerLoop");

            var containsKeyMethodInfo =
                source.GetType().GetMethod("ContainsKey", BindingFlags.Instance | BindingFlags.Public);

            var toArrayMethodInfo = typeof(Enumerable).GetMethods()
                .Single(m => m.Name == "ToArray" && m.GetParameters().Length == 1).MakeGenericMethod(typeof(CustomAttributeData));

            var getValueOrDefaultMethodInfo = typeof(CollectionExtensions).GetMethods()
                .Single(m => m.Name == "GetValueOrDefault" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string), typeof(object));

            var setValueMethodInfo = typeof(PropertyInfo).GetMethod("SetValue", new[] {typeof(object), typeof(object)});

            var entityParameter = Expression.Parameter(entity.GetType(), "entity");
            var sourceParameter = Expression.Parameter(typeof(Dictionary<string, object>), "source");


            var customAttribute = Expression.Parameter(typeof(CustomAttributeData), "customAttribute");

            var isIgnoreAttributeDefined = Expression.Parameter(typeof(bool), "isIgnoreAttributeDefined");

            var customAttributesArray =
                Expression.Parameter(typeof(CustomAttributeData[]), "customAttributesArray");

            var block = Expression.Block(
                new[] { i, propertiesArrayLength },
                Expression.Assign(i, Expression.Constant(0)),
                Expression.Assign(propertiesArrayLength, Expression.Property(properties, "Length")),
                Expression.Loop(
                    Expression.Block(
                        new[] { property },
                        Expression.IfThenElse(
                            Expression.Not(Expression.LessThanOrEqual(i, Expression.Subtract(propertiesArrayLength, Expression.Constant(1)))),
                            Expression.Break(endOuterLoop),
                            Expression.Assign(property, Expression.ArrayIndex(properties, i))),
                        Expression.IfThen(
                            Expression.AndAlso(
                                Expression.Block(
                                    new[] { j, customAttributesLength, customAttributesArray, isIgnoreAttributeDefined },
                                    Expression.Assign(j, Expression.Constant(0)),
                                    Expression.Assign(customAttributesArray, Expression.Call(toArrayMethodInfo, Expression.Property(property, "CustomAttributes"))),
                                    Expression.Assign(customAttributesLength, Expression.Property(customAttributesArray, "Length")),
                                    Expression.Assign(isIgnoreAttributeDefined, Expression.Constant(false)),
                                    Expression.Loop(
                                        Expression.Block(
                                            new[]{ customAttribute },
                                            Expression.IfThenElse(
                                                Expression.OrElse(
                                                    isIgnoreAttributeDefined,
                                                    Expression.Not(Expression.LessThanOrEqual(j, Expression.Subtract(customAttributesLength, Expression.Constant(1))))),
                                                Expression.Break(endInnerLoop),
                                                Expression.Block(
                                                    Expression.Assign(customAttribute, Expression.ArrayIndex(customAttributesArray, j)),
                                                    Expression.Assign(isIgnoreAttributeDefined, Expression.Equal(Expression.Property(customAttribute, "AttributeType"), Expression.Constant(typeof(IgnoreUpdateAttribute), typeof(Type)))))),
                                            Expression.PostIncrementAssign(i)), endInnerLoop),
                                    Expression.Not(isIgnoreAttributeDefined)), 
                                Expression.Call(sourceParameter, containsKeyMethodInfo, Expression.Property(property, "Name"))),
                            Expression.Block(
                                new[] { value },
                                Expression.Assign(value, Expression.Call(getValueOrDefaultMethodInfo, sourceParameter, Expression.Property(property, "Name"))),
                                Expression.Call(property, setValueMethodInfo, entityParameter, value))),
                        Expression.PostIncrementAssign(i)), endOuterLoop));

            // TODO Put it to the cache
            var expr = Expression.Lambda<Action<TEntity, Dictionary<string, object>>>(block, entityParameter, sourceParameter).Compile();

            expr(entity, source);
        }
    }
}