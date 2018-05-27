using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ReadingList.ReadModel
{
    public class ReadDbConnection
    {
        private readonly IDbConnection _connection;

        public ReadDbConnection(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<T> QuerySingle<T>(string query, object param = null)
        {
            return await _connection.QuerySingleAsync<T>(query, param);
        }
        
        public async Task<T> QueryFirst<T>(string query, object param = null)
        {
            return await _connection.QueryFirstAsync<T>(query, param);
        }

        public async Task<TR> QuerySingle<TP, TS, TR>(string query, Func<TP, TS, TR> map, object param = null)
        {
            return (await _connection.QueryAsync(query, map, param)).SingleOrDefault();
        }
        
        public async Task<TR> QueryFirst<TP, TS, TR>(string query, Func<TP, TS, TR> map, object param = null)
        {
            return (await _connection.QueryAsync(query, map, param)).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> Query<T>(string query, object param = null)
        {
            return await _connection.QueryAsync<T>(query, param);
        }

        public async Task<int> Execute(string query)
        {
            return await _connection.ExecuteAsync(query);
        }
    }
}
