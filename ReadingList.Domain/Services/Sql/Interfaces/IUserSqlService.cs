namespace ReadingList.Domain.Services.Sql.Interfaces
{
    public interface IUserSqlService
    {
        string GetUserByLoginSql(string parameterName = "login");

        string GetUserByIdSql(string parameterName = "id");

        string GetUserByLoginAndPasswordSql(string loginParameterName = "login",
            string passwordParameterName = "password");
    }
}