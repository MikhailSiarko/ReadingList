using Cinch.SqlBuilder;
using ReadingList.Domain.Services.Sql.Interfaces;

namespace ReadingList.Domain.Services.Sql
{
    public class UserSqlService : IUserSqlService
    {
        public const string UserIdSql = "SELECT Id FROM Users WHERE Login = @login";

        private static ISqlBuilder CreateSqlBuilder() => new SqlBuilder()
            .Select("Id", "Login", "Password", "ProfileId", "(SELECT Name FROM Roles WHERE Id = RoleId) AS Role")
            .From("Users");

        public string GetUserByLoginSqlQuery() => CreateSqlBuilder().Where("Login = @login").ToSql();            

        public string GetUserByIdSqlQuery() => CreateSqlBuilder().Where("Id = @id").ToSql();

        public string GetUserByLoginAndPasswordSqlQuery() =>
            CreateSqlBuilder().Where("Login = @login").Where("Password = @password").ToSql();
    }
}