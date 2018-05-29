using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.FluentSqlBuilder;
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
            var sqlResult = FluentSqlBuilder.NewBuilder()
                .Select("Id, Login")
                .From("Users")
                .Where("Id = @id")
                .Build();

            return await _dbConnection.QuerySingleAsync<UserRM>(sqlResult.RawSql, sqlResult.Parameters);
        }
    }
}