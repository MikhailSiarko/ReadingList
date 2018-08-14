using System;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<Type, object> _cache;
        private readonly MethodInfo _getValueOrDefaultMethodInfo;
        private readonly MethodInfo _containsKeyMethodInfo;

        public EntityUpdateService()
        {
            _cache = new ConcurrentDictionary<Type, object>();

            _getValueOrDefaultMethodInfo = typeof(CollectionExtensions)
                .GetMethods().Single(m => m.Name == "GetValueOrDefault" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string), typeof(object));

            _containsKeyMethodInfo = typeof(Dictionary<string, object>).GetMethod("ContainsKey");
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
            var entityType = typeof(TEntity);

            if (_cache.TryGetValue(entityType, out var updater))
            {
                return (Action<TEntity, Dictionary<string, object>>) updater;
            }

            RegisterUpdater<TEntity>();
            _cache.TryGetValue(entityType, out updater);

            return (Action<TEntity, Dictionary<string, object>>) updater;
        }

        private void RegisterUpdater<TEntity>() where TEntity : Entity
        {
            var entityType = typeof(TEntity);

            var propertyInfos = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p =>
                p.CustomAttributes.All(a => a.AttributeType != typeof(IgnoreUpdateAttribute))).ToList();

            var entityParameter = Expression.Parameter(entityType, "entity");
            var sourceParameter = Expression.Parameter(typeof(Dictionary<string, object>), "source");

            var block = Expression.Block(BuildExpressionSequence(propertyInfos, entityParameter, sourceParameter));

            var updater = Expression.Lambda<Action<TEntity, Dictionary<string, object>>>(block, entityParameter, sourceParameter).Compile();

            _cache.TryAdd(entityType, updater);
        }

        private IEnumerable<Expression> BuildExpressionSequence(IEnumerable<PropertyInfo> propertyInfos, Expression entityParameter, Expression sourceParameter)
        {
            var expressions = new List<Expression>();

            foreach (var propertyInfo in propertyInfos)
            {
                var propertyName = Expression.Constant(propertyInfo.Name);
                var propertyExpression = Expression.PropertyOrField(entityParameter, propertyInfo.Name);
                expressions.Add(Expression.IfThen(
                    Expression.Call(sourceParameter, _containsKeyMethodInfo, propertyName),
                    Expression.Assign(
                        propertyExpression,
                        Expression.Convert(Expression.Call(_getValueOrDefaultMethodInfo, sourceParameter, propertyName), propertyInfo.PropertyType))));
            }

            return expressions;
        }
    }
}