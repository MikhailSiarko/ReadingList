namespace ReadingList.Domain.Services.Sql.Interfaces
{
    public interface IPrivateBookListSqlService
    {
        string GetPrivateBookListSqlQuery();

        string GetPrivateBookListItemSqlQuery();
    }
}