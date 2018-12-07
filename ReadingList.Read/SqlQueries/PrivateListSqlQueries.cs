using Cinch.SqlBuilder;
using ReadingList.Models.Write;

namespace ReadingList.Read.SqlQueries
{
    public static class PrivateListSqlQueries
    {
        public static string SelectByLogin => new SqlBuilder()
            .Select("l.Id", "l.Name", "l.OwnerId", "l.Type", "i.Id", "i.ReadingTimeInSeconds", "i.Title", "i.Author",
                "i.Status", "i.BookListId AS ListId", "i.BookId")
            .From("BookLists AS l")
            .LeftJoin("(" +
                      new SqlBuilder()
                          .Select("pi.Id", "b.Title", "b.Author", "pi.BookId", "pi.BookListId", "pi.Status",
                              "pi.ReadingTimeInSeconds")
                          .From("PrivateBookListItems AS pi")
                          .LeftJoin("Books AS b ON pi.BookId = b.Id")
                          .ToSql() +
                      ") AS i ON i.BookListId = l.Id")
            .Where("l.OwnerId = @UserId")
            .Where($"l.Type = {BookListType.Private:D}")
            .ToSql();
    }
}