using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace ReadingList.ReadModel.DbConnection
{
    public class ReadDbConnection : IReadDbConnection
    {
        private readonly IDbConnection _dbConnection;

        public ReadDbConnection(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> QuerySingleAsync<T>(string query, object param = null)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(query, param);
        }
        
        public async Task<T> QueryFirstAsync<T>(string query, object param = null)
        {
            return  await _dbConnection.QueryFirstOrDefaultAsync<T>(query, param);
        }

        public async Task<TResult> QuerySingleAsync<TSource1, TSource2, TResult>(string query, Func<TSource1, TSource2, TResult> map, object param = null)
        {
            return (await _dbConnection.QueryAsync(query, map, param)).SingleOrDefault();
        }
        
        public async Task<TResult> QueryFirstAsync<TSource1, TSource2, TResult>(string query, Func<TSource1, TSource2, TResult> map, object param = null)
        {
            return (await _dbConnection.QueryAsync(query, map, param)).FirstOrDefault();
        }
        
        public async Task<T> QuerySingleAsync<T>(string query, Func<SqlMapper.GridReader, Task<T>> func, object param = null) 
        {
            T item;
            
            using (var reader = await _dbConnection.QueryMultipleAsync(query, param))
            {
                item = await func(reader);
            }

            return item;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null)
        {
            return await _dbConnection.QueryAsync<T>(query, param);
        }
        
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, Func<SqlMapper.GridReader, Task<List<T>>> func, object param = null)
        {
            List<T> list;
            
            using (var reader = await _dbConnection.QueryMultipleAsync(query, param))
            {
                list = await func(reader);
            }

            return list;
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}
