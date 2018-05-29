using System.Threading.Tasks;
using Awesome.Data.Sql.Builder;
using Awesome.Data.Sql.Builder.Renderers;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using UserRM = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserRM>
    {
        private readonly IReadDbConnection _dbConnection;

        public GetUserQueryHandler(IReadDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override async Task<UserRM> Handle(GetUserQuery query)
        {
            var sql = SqlStatements.Select("Id", "Login")
                .From("Users")
                .Where("Id = @id")
                .ToSql(new SqlServerSqlRenderer());

            return await _dbConnection.QuerySingleAsync<UserRM>(sql, new {id = query.UserId});
        }
    }
}