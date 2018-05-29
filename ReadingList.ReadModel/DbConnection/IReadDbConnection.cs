using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingList.ReadModel.DbConnection
{
    public interface IReadDbConnection
    {
        Task<T> QuerySingleAsync<T>(string query, object param = null);

        Task<T> QueryFirstAsync<T>(string query, object param = null);

        Task<TR> QuerySingleAsync<TP, TS, TR>(string query, Func<TP, TS, TR> map, object param = null);

        Task<TR> QueryFirstAsync<TP, TS, TR>(string query, Func<TP, TS, TR> map, object param = null);

        Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null);
    }
}