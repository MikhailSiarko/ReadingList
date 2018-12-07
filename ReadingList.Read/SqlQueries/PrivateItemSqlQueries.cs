using Cinch.SqlBuilder;
using ReadingList.Models.Write;

namespace ReadingList.Read.SqlQueries
{
    public static class PrivateItemSqlQueries
    {
        public static string SelectById => new SqlBuilder()
            .Select("i.Id", "b.Title", "b.Author", "b.GenreId", "i.Status", "i.ReadingTimeInSeconds", "i.BookId")
            .From("PrivateBookListItems AS i")
            .LeftJoin("Books AS b ON i.BookId = b.Id")
            .Where("BookListId = (" +
                   new SqlBuilder()
                       .Select("l.Id")
                       .From("BookLists AS l")
                       .Where("l.OwnerId = @UserId")
                       .Where($"l.Type = {BookListType.Private:D}")
                       .ToSql() +
                   ")")
            .Where("i.Id = @ItemId")
            .ToSql();
    }
}