using Cinch.SqlBuilder;

namespace ReadingList.Read.SqlQueries
{
    public static class BookSqlQueries
    {
        public static string FindBooks => new SqlBuilder()
            .Select("DISTINCT b.Id", "b.Author", "b.Title", "g.Name AS Genre", "t.Name AS Tag")
            .From("Books AS b")
            .LeftJoin("BookTags AS bt on b.Id = bt.BookId")
            .LeftJoin("Tags AS t on bt.TagId = t.Id")
            .LeftJoin("Genres AS g on g.Id = b.GenreId")
            .Where("CASE " +
                   "WHEN @Query IS NULL OR @Query = '' THEN (1 + 1)" +
                   "WHEN @Query LIKE '#%' THEN lower(t.Name) LIKE '%' || substr(@Query, 2) || '%' " +
                   "ELSE lower(b.Author) LIKE '%' || @Query || '%' OR lower(b.Title) LIKE '%' || @Query || '%' " +
                   "END")
            .OrderBy("b.Id " +
                     "LIMIT @Count + 1 " +
                     "OFFSET (@Count * (@Chunk - 1))")
            .ToSql();
    }
}