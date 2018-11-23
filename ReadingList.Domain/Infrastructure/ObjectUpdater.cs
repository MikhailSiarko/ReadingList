using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ReadingList.Domain.Infrastructure
{
    internal static class ObjectUpdater<T>
    {
        private static readonly Action<T, Dictionary<string, object>> Action;

        static ObjectUpdater()
        {
            Action = InitializeUpdater();
        }

        public static void Update(T obj, Dictionary<string, object> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                return;

            Action(obj, source);
        }

        private static Action<T, Dictionary<string, object>> InitializeUpdater()
        {
            var entityType = typeof(T);

            var propertyInfos = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p =>
                p.GetCustomAttribute<IgnoreUpdateAttribute>(true) == null).ToList();

            var entityParameter = Expression.Parameter(entityType, "entity");
            var sourceParameter = Expression.Parameter(typeof(Dictionary<string, object>), "source");

            var block = Expression.Block(BuildExpressionSequence(propertyInfos, entityParameter, sourceParameter));

            return Expression
                .Lambda<Action<T, Dictionary<string, object>>>(block, entityParameter, sourceParameter).Compile();
        }

        private static IEnumerable<Expression> BuildExpressionSequence(IEnumerable<PropertyInfo> propertyInfos,
            Expression entityParameter, Expression sourceParameter)
        {
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyName = Expression.Constant(propertyInfo.Name);
                var propertyExpression = Expression.PropertyOrField(entityParameter, propertyInfo.Name);
                yield return Expression.IfThen(
                    Expression.Call(sourceParameter, DictionaryMethodInfos.ContainsKey, propertyName),
                    Expression.Assign(
                        propertyExpression,
                        Expression.Convert(
                            Expression.Call(DictionaryMethodInfos.GetValueOrDefault, sourceParameter,
                                propertyName),
                            propertyInfo.PropertyType)));
            }
        }
    }

    internal static class DictionaryMethodInfos
    {
        public static readonly MethodInfo GetValueOrDefault;

        public static readonly MethodInfo ContainsKey;

        static DictionaryMethodInfos()
        {
            GetValueOrDefault = typeof(CollectionExtensions)
                .GetMethods().Single(m => m.Name == "GetValueOrDefault" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string), typeof(object));

            ContainsKey = typeof(Dictionary<string, object>).GetMethod("ContainsKey");
        }
    }
}