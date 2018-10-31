using Cinch.SqlBuilder;
using ReadingList.Domain.Enumerations;

namespace ReadingList.Read.SqlQueries
{
    public static class PrivateItemSqlQueries
    {
        public static string SelectById => new SqlBuilder()
            .Select("Id", "Title", "Author", "Status", "ReadingTimeInSeconds")
            .From("PrivateBookListItems")
            .Where($"BookListId = (SELECT Id FROM BookLists WHERE OwnerId = ({UserSqlQueries.SelectIdByLogin})")
            .Where($"Type = {BookListType.Private:D}) AND Id = @ItemId")
            .ToSql();
    }
}