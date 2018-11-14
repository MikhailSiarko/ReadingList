using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;

namespace ReadingList.Read
{
    public class SqlQueryContext<TQuery, TResult>
        where TQuery : class, IRequest<TResult> 
    {
        public string Sql { get; }

        public IReadOnlyDictionary<string, object> Parameters => _queryType
            .GetFields(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(x => x.Name, x => x.GetValue(Query));
        
        public TQuery Query { get; }

        private readonly Type _queryType;

        public SqlQueryContext(TQuery query)
        {
            Query = query;
            
            _queryType = query.GetType();

            if (ReadQueriesRegistry.TryGetSql(_queryType, out var sql))
            {
                Sql = sql;
            }
            else
            {
                throw new KeyNotFoundException("Appropriate SQL query wasn't found");
            }
        }
    }
}