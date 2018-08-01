using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.Domain.Services
{
    public class EntityUpdateService : IEntityUpdateService
    {
        private readonly Dictionary<Type, object> _cache;

        public EntityUpdateService()
        {
            _cache = new Dictionary<Type, object>();
        }

        public void Update<TEntity>(TEntity entity, Dictionary<string, object> source) where TEntity : Entity
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                return;
            
            var updater = GetUpdater<TEntity>();
            updater(entity, source);
        }

        private Action<TEntity, Dictionary<string, object>> GetUpdater<TEntity>() where TEntity : Entity
        {
            if (_cache.ContainsKey(typeof(TEntity)))
            {
                return (Action<TEntity, Dictionary<string, object>>) _cache.GetValueOrDefault(typeof(TEntity));
            }
            RegisterUpdater<TEntity>();
            return (Action<TEntity, Dictionary<string, object>>) _cache.GetValueOrDefault(typeof(TEntity));
        }

        private void RegisterUpdater<TEntity>() where TEntity : Entity
        {
            var i = Expression.Parameter(typeof(int), "i");
            var j = Expression.Parameter(typeof(int), "j");

            var propertiesArrayLength = Expression.Parameter(typeof(int), "propertiesArrayLength");
            var customAttributesLength = Expression.Parameter(typeof(int), "customAttributesLength");

            var property = Expression.Parameter(typeof(PropertyInfo), "property");
            var value = Expression.Parameter(typeof(object), "value");

            var entityParameter = Expression.Parameter(typeof(TEntity), "entity");
            var sourceParameter = Expression.Parameter(typeof(Dictionary<string, object>), "source");

            var entityType = Expression.Parameter(typeof(Type), "entityType");
            var properties = Expression.Parameter(typeof(PropertyInfo[]), "properties");

            var customAttribute = Expression.Parameter(typeof(CustomAttributeData), "customAttribute");
            var isIgnoreAttributeDefined = Expression.Parameter(typeof(bool), "isIgnoreAttributeDefined");
            var customAttributesArray =
                Expression.Parameter(typeof(CustomAttributeData[]), "customAttributesArray");

            var endOuterLoop = Expression.Label("endOuterLoop");
            var endInnerLoop = Expression.Label("endInnerLoop");

            var block = Expression.Block(
                new[] {i, propertiesArrayLength, entityType, properties},
                Expression.Assign(entityType, Expression.Call(entityParameter, typeof(TEntity).GetMethod("GetType"))),
                Expression.Assign(properties,
                    Expression.Call(entityType,
                        typeof(Type).GetMethods()
                            .Single(m => m.Name == "GetProperties" && m.GetParameters().Length == 0))),
                Expression.Assign(i, Expression.Constant(0)),
                Expression.Assign(propertiesArrayLength, Expression.Property(properties, "Length")),
                Expression.Loop(
                    Expression.Block(
                        new[] {property},
                        Expression.IfThenElse(
                            Expression.Not(Expression.LessThanOrEqual(i,
                                Expression.Subtract(propertiesArrayLength, Expression.Constant(1)))),
                            Expression.Break(endOuterLoop),
                            Expression.Assign(property, Expression.ArrayIndex(properties, i))),
                        Expression.IfThen(
                            Expression.AndAlso(
                                Expression.Block(
                                    new[] {j, customAttributesLength, customAttributesArray, isIgnoreAttributeDefined},
                                    Expression.Assign(j, Expression.Constant(0)),
                                    Expression.Assign(customAttributesArray,
                                        Expression.Call(typeof(Enumerable).GetMethods()
                                                .Single(m => m.Name == "ToArray" && m.GetParameters().Length == 1)
                                                .MakeGenericMethod(typeof(CustomAttributeData)),
                                            Expression.Property(property, "CustomAttributes"))),
                                    Expression.Assign(customAttributesLength,
                                        Expression.Property(customAttributesArray, "Length")),
                                    Expression.Assign(isIgnoreAttributeDefined, Expression.Constant(false)),
                                    Expression.Loop(
                                        Expression.Block(
                                            new[] {customAttribute},
                                            Expression.IfThenElse(
                                                Expression.OrElse(
                                                    isIgnoreAttributeDefined,
                                                    Expression.Not(Expression.LessThanOrEqual(j,
                                                        Expression.Subtract(customAttributesLength,
                                                            Expression.Constant(1))))),
                                                Expression.Break(endInnerLoop),
                                                Expression.Block(
                                                    Expression.Assign(customAttribute,
                                                        Expression.ArrayIndex(customAttributesArray, j)),
                                                    Expression.Assign(isIgnoreAttributeDefined, Expression.Equal(
                                                        Expression.Property(customAttribute, "AttributeType"),
                                                        Expression.Constant(typeof(IgnoreUpdateAttribute),
                                                            typeof(Type)))))),
                                            Expression.PostIncrementAssign(i)), endInnerLoop),
                                    Expression.Not(isIgnoreAttributeDefined)),
                                Expression.Call(sourceParameter,
                                    typeof(Dictionary<string, object>).GetMethod("ContainsKey"),
                                    Expression.Property(property, "Name"))),
                            Expression.Block(
                                new[] {value},
                                Expression.Assign(value,
                                    Expression.Call(typeof(CollectionExtensions).GetMethods().Single(m =>
                                                m.Name == "GetValueOrDefault" && m.GetParameters().Length == 2)
                                            .MakeGenericMethod(typeof(string), typeof(object)), sourceParameter,
                                        Expression.Property(property, "Name"))),
                                Expression.Call(property,
                                    typeof(PropertyInfo).GetMethod("SetValue", new[] {typeof(object), typeof(object)}),
                                    entityParameter, value))),
                        Expression.PostIncrementAssign(i)), endOuterLoop));

            var updater = Expression.Lambda<Action<TEntity, Dictionary<string, object>>>(block, entityParameter, sourceParameter).Compile();

            _cache.Add(typeof(TEntity), updater);
        }
    }
}
