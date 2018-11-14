using Cinch.SqlBuilder;

namespace ReadingList.Read.SqlQueries
{
    public static class UserSqlQueries
    {
        public static string SelectById => CreateSqlBuilder().Where("Id = @UserId").ToSql();
        
        public static string SelectByLogin => CreateSqlBuilder().Where("Login = @Login").ToSql();
        
        private static ISqlBuilder CreateSqlBuilder() => new SqlBuilder()
            .Select("Id", "Login", "Password", "ProfileId", "(SELECT Name FROM Roles WHERE Id = RoleId) AS Role")
            .From("Users");
    }
}