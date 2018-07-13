namespace ReadingList.Domain.Services.Sql.Interfaces
{
    public interface IUserSqlService
    {
        string GetUserByLoginSqlQuery();

        string GetUserByIdSqlQuery();

        string GetUserByLoginAndPasswordSqlQuery();
    }
}