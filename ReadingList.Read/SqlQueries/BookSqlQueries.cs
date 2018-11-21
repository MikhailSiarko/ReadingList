using Cinch.SqlBuilder;

namespace ReadingList.Read.SqlQueries
{
    public static class BookSqlQueries
    {
        public static string FindBooks => new SqlBuilder()
            .Select("DISTINCT b.Id", "b.Author", "b.Title", "t.Name AS Tag")
            .From("Books AS b")
            .LeftJoin("BookTags AS bt on b.Id = bt.BookId")
            .LeftJoin("Tags AS t on bt.TagId = t.Id")
            .Where("CASE " +
                   "WHEN @Query LIKE '#%' THEN lower(t.Name) LIKE '%' || substr(@Query, 2) || '%' " +
                   "ELSE lower(b.Author) LIKE '%' || @Query || '%' OR lower(b.Title) LIKE '%' || @Query || '%' " +
                   "END")
            .ToSql();
    }
}