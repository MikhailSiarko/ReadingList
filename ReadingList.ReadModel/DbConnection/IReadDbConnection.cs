using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingList.ReadModel.DbConnection
{
    public interface IReadDbConnection : IDisposable
    {
        Task<T> QuerySingleAsync<T>(string query, object param = null);

        Task<T> QueryFirstAsync<T>(string query, object param = null);

        Task<TResult> QuerySingleAsync<TSource1, TSource2, TResult>(string query, Func<TSource1, TSource2, TResult> map, object param = null);

        Task<TResult> QueryFirstAsync<TSource1, TSource2, TResult>(string query, Func<TSource1, TSource2, TResult> map, object param = null);

        Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null);
    }
}