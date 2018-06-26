namespace ReadingList.Domain.Services.Sql.Interfaces
{
    public interface IUserSqlService
    {
        string GetUserByLoginSql();

        string GetUserByIdSql();

        string GetUserByLoginAndPasswordSql();
    }
}