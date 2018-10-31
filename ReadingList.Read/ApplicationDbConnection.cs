using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace ReadingList.Read
{
    public class ApplicationDbConnection : IApplicationDbConnection
    {
        private readonly IDbConnection _dbConnection;

        public ApplicationDbConnection(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> QuerySingleAsync<T>(ISqlQueryContext queryContext)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(queryContext.Sql, queryContext.QueryParameters);
        }
        
        public async Task<T> QueryFirstAsync<T>(ISqlQueryContext queryContext)
        {
            return  await _dbConnection.QueryFirstOrDefaultAsync<T>(queryContext.Sql, queryContext.QueryParameters);
        }

        public async Task<TResult> QuerySingleAsync<TSource1, TSource2, TResult>(ISqlQueryContext queryContext, Func<TSource1, TSource2, TResult> map)
        {
            return (await _dbConnection.QueryAsync(queryContext.Sql, map, queryContext.QueryParameters))
                .SingleOrDefault();
        }
        
        public async Task<TResult> QueryFirstAsync<TSource1, TSource2, TResult>(ISqlQueryContext queryContext, Func<TSource1, TSource2, TResult> map)
        {
            return (await _dbConnection.QueryAsync(queryContext.Sql, map, queryContext.QueryParameters))
                .FirstOrDefault();
        }

        public async Task<T> QuerySingleAsync<T>(ISqlQueryContext queryContext, Func<SqlMapper.GridReader, Task<T>> func) 
        {
            T item;
            
            using (var reader = await _dbConnection.QueryMultipleAsync(queryContext.Sql, queryContext.QueryParameters))
            {
                item = await func(reader);
            }

            return item;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(ISqlQueryContext queryContext)
        {
            return await _dbConnection.QueryAsync<T>(queryContext.Sql, queryContext.QueryParameters);
        }
        
        public async Task<IEnumerable<T>> QueryAsync<T>(ISqlQueryContext queryContext, Func<SqlMapper.GridReader, Task<List<T>>> func)
        {
            List<T> items;
            
            using (var reader = await _dbConnection.QueryMultipleAsync(queryContext.Sql, queryContext.QueryParameters))
            {
                items = await func(reader);
            }

            return items;
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}