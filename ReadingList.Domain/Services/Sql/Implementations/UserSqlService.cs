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

        public string GetUserByLoginSql() =>
            _sqlBuilder.Where("Login = @login").ToSql();

        public string GetUserByIdSql() =>
            _sqlBuilder.Where("Id = @id").ToSql();

        public string GetUserByLoginAndPasswordSql() =>
            _sqlBuilder.Where("Login = @login").Where($"Password = @password").ToSql();
    }
}