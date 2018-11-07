using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ReadingList.Domain.Infrastructure
{
    public static class ObjectUpdater
    {
        private static readonly ConcurrentDictionary<Type, object> Cache;
        private static readonly MethodInfo GetValueOrDefaultMethodInfo;
        private static readonly MethodInfo ContainsKeyMethodInfo;

        static ObjectUpdater()
        {
            Cache = new ConcurrentDictionary<Type, object>();

            GetValueOrDefaultMethodInfo = typeof(CollectionExtensions)
                .GetMethods().Single(m => m.Name == "GetValueOrDefault" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string), typeof(object));

            ContainsKeyMethodInfo = typeof(Dictionary<string, object>).GetMethod("ContainsKey");
        }

        public static void Update<TEntity>(TEntity entity, Dictionary<string, object> source)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                return;
            
            var updater = GetUpdater<TEntity>();
            updater(entity, source);
        }

        private static Action<TEntity, Dictionary<string, object>> GetUpdater<TEntity>()
        {
            return (Action<TEntity, Dictionary<string, object>>) Cache.GetOrAdd(typeof(TEntity),
                _ => RegisterUpdater<TEntity>());
        }

        private static Action<TEntity, Dictionary<string, object>> RegisterUpdater<TEntity>()
        {
            var entityType = typeof(TEntity);

            var propertyInfos = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p =>
                p.GetCustomAttribute<IgnoreUpdateAttribute>(true) == null).ToList();

            var entityParameter = Expression.Parameter(entityType, "entity");
            var sourceParameter = Expression.Parameter(typeof(Dictionary<string, object>), "source");

            var block = Expression.Block(BuildExpressionSequence(propertyInfos, entityParameter, sourceParameter));

            return Expression.Lambda<Action<TEntity, Dictionary<string, object>>>(block, entityParameter, sourceParameter).Compile();
        }

        private static IEnumerable<Expression> BuildExpressionSequence(IEnumerable<PropertyInfo> propertyInfos, Expression entityParameter, Expression sourceParameter)
        {
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyName = Expression.Constant(propertyInfo.Name);
                var propertyExpression = Expression.PropertyOrField(entityParameter, propertyInfo.Name);
                yield return Expression.IfThen(
                    Expression.Call(sourceParameter, ContainsKeyMethodInfo, propertyName),
                    Expression.Assign(
                        propertyExpression,
                        Expression.Convert(Expression.Call(GetValueOrDefaultMethodInfo, sourceParameter, propertyName), propertyInfo.PropertyType)));
            }
        }
    }
}