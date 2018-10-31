using System.Reflection;
using Dapper;
using MediatR;
using ReadingList.Read;

namespace ReadingList.Application.Queries
{
    public abstract class Query<TResult> : IRequest<TResult>, IQuery
    {
        public ISqlQueryContext SqlQueryContext { get; private set; }

        public void InitializeSqlQueryContext(string sql)
        {
            var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            var parameters = new DynamicParameters();

            foreach (var fieldInfo in fields)
            {
                parameters.Add(fieldInfo.Name, fieldInfo.GetValue(this));
            }
            
            SqlQueryContext = new SqlQueryContext(sql, parameters);
        }
    }
}