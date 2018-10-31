using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace ReadingList.Read
{
    public interface IApplicationDbConnection : IDisposable
    {
        Task<T> QuerySingleAsync<T>(ISqlQueryContext queryContext);

        Task<T> QueryFirstAsync<T>(ISqlQueryContext queryContext);

        Task<TResult> QuerySingleAsync<TSource1, TSource2, TResult>(ISqlQueryContext queryContext, Func<TSource1, TSource2, TResult> map);

        Task<T> QuerySingleAsync<T>(ISqlQueryContext queryContext, Func<SqlMapper.GridReader, Task<T>> action);

        Task<TResult> QueryFirstAsync<TSource1, TSource2, TResult>(ISqlQueryContext queryContext, Func<TSource1, TSource2, TResult> map);

        Task<IEnumerable<T>> QueryAsync<T>(ISqlQueryContext queryContext);

        Task<IEnumerable<T>> QueryAsync<T>(ISqlQueryContext queryContext, Func<SqlMapper.GridReader, Task<List<T>>> action);
    }
}