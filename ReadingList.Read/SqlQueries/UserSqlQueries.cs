using Cinch.SqlBuilder;
using ReadingList.Models.Write;

namespace ReadingList.Read.SqlQueries
{
    public static class UserSqlQueries
    {
        public static string SelectById => CreateSqlBuilder().Where("Id = @UserId").ToSql();

        public static string SelectModerators => new SqlBuilder()
            .Select("Id", "Login")
            .From("Users")
            .Where($"RoleId = {UserRole.User:D}")
            .Where("Id <> @UserId")
            .ToSql();

        private static ISqlBuilder CreateSqlBuilder() => new SqlBuilder()
            .Select("Id", "Login", "Password", "ProfileId", "(SELECT Name FROM Roles WHERE Id = RoleId) AS Role")
            .From("Users");
    }
}