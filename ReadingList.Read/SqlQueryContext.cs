using Dapper;

namespace ReadingList.Read
{
    public class SqlQueryContext : ISqlQueryContext
    {
        public SqlQueryContext(string sql, DynamicParameters queryParameters)
        {
            Sql = sql;
            QueryParameters = queryParameters;
        }
        
        public DynamicParameters QueryParameters { get; }
        
        public string Sql { get; }
    }
}