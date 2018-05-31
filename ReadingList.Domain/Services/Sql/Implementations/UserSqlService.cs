using Cinch.SqlBuilder;
using ReadingList.Domain.Services.Sql.Interfaces;

namespace ReadingList.Domain.Services.Sql
{
    public class UserSqlService : IUserSqlService
    {
        private readonly ISqlBuilder _sqlBuilder;

        public UserSqlService()
        {
            _sqlBuilder = new SqlBuilder()
                .Select("Id", "Login", "Password", "ProfileId", "(SELECT Name FROM Roles WHERE Id = RoleId) AS Role")
                .From("Users");
        }

        public string GetUserByLoginSql(string parameterName = "login") =>
            _sqlBuilder.Where($"Login = @{parameterName}").ToSql();

        public string GetUserByIdSql(string parameterName = "id") =>
            _sqlBuilder.Where($"Id = @{parameterName}").ToSql();

        public string GetUserByLoginAndPasswordSql(string loginParameterName = "login",
            string passwordParameterName = "password") =>
            _sqlBuilder.Where($"Login = @{loginParameterName}").Where($"Password = @{passwordParameterName}").ToSql();
    }
}