using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel;
using UserRM = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserRM>
    {
        private readonly ReadDbConnection _dbConnection;

        public GetUserQueryHandler(ReadDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override async Task<UserRM> Process(GetUserQuery query)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder
                .Select("Id, Login From Users").Where("Id = @id").AddParameters(new {id = query.UserId});
            var getUserQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM Users /**where**/");
            return await _dbConnection.QuerySingleAsync<UserRM>(getUserQuery.RawSql, getUserQuery.Parameters);
        }
    }
}