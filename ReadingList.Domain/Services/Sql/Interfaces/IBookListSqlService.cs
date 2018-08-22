namespace ReadingList.Domain.Services.Sql.Interfaces
{
    public interface IBookListSqlService
    {
        string GetBookListSqlQuery();

        string GetBookListItemSqlQuery();
    }
}