using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ReadingList.ReadModel.DbConnection
{
    public class ReadDbConnection : IReadDbConnection
    {
        private readonly IConfiguration _configuration;

        public ReadDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<T> QuerySingleAsync<T>(string query, object param = null)
        {
            return await MakeQuery(query, param, async (q, p, c) => await c.QuerySingleAsync<T>(q, p));
        }
        
        public async Task<T> QueryFirstAsync<T>(string query, object param = null)
        {
            return await MakeQuery(query, param, async (q, p, c) => await c.QueryFirstAsync<T>(q, p));
        }

        public async Task<TR> QuerySingleAsync<TP, TS, TR>(string query, Func<TP, TS, TR> map, object param = null)
        {
            return await MakeQuery(query, param,
                async (q, p, c) => (await c.QueryAsync(q, map, p)).SingleOrDefault());
        }
        
        public async Task<TR> QueryFirstAsync<TP, TS, TR>(string query, Func<TP, TS, TR> map, object param = null)
        {
            return await MakeQuery(query, param,
                async (q, p, c) => (await c.QueryAsync(q, map, p)).FirstOrDefault());
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null)
        {
            return await MakeQuery(query, param, async (q, p, c) => await c.QueryAsync<T>(q, p));
        }

        private async Task<T> MakeQuery<T>(string query, object param, Func<string, object, IDbConnection, Task<T>> func)
        {
            T result;          
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                result = await func(query, param, connection);
            }
            return result;
        }
    }
}
