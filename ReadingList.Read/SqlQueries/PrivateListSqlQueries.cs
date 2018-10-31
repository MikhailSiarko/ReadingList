using Cinch.SqlBuilder;
using ReadingList.Domain.Enumerations;

namespace ReadingList.Read.SqlQueries
{
    public static class PrivateListSqlQueries
    {
        public static string SelectByLogin => new SqlBuilder()
            .Select("l.Id", "l.Name", "l.OwnerId", "l.Type", "i.Id", "i.ReadingTimeInSeconds", "i.Title", "i.Author",
                "i.Status", "i.BookListId AS ListId")
            .From("BookLists AS l")
            .LeftJoin(
                "(SELECT Id, Title, Author, BookListId, Status, ReadingTimeInSeconds FROM PrivateBookListItems) AS i ON i.BookListId = l.Id")
            .Where($"l.OwnerId = ({UserSqlQueries.SelectIdByLogin})")
            .Where($"l.Type = {BookListType.Private:D}")
            .ToSql();
    }
}