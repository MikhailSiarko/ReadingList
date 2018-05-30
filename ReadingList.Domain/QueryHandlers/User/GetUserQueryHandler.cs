using System.Threading.Tasks;
using Cinch.SqlBuilder;
using ReadingList.Domain.Abstractions;
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
            var sql = new SqlBuilder().Select("Id", "Login")
                .From("Users")
                .Where("Id = @id")
                .ToSql();

            return await _dbConnection.QuerySingleAsync<UserRM>(sql, new {id = query.UserId});
        }
    }
}