namespace ReadingList.Domain.Services.Sql.Interfaces
{
    public interface ISharedBookListSqlService : IBookListSqlService
    {
        string GetListsSqlQuery();
    }
}