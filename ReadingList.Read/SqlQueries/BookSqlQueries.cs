namespace ReadingList.Read.SqlQueries
{
    public static class BookSqlQueries
    {
        public static string FindBooks => "SELECT DISTINCT b.Id, b.Author, b.Title From Books AS b " +
                                          "LEFT JOIN BookTags AS bt on b.Id = bt.BookId " +
                                          "LEFT JOIN Tags AS t on bt.TagId = t.Id " +
                                          "WHERE CASE " +
                                          "WHEN @Query LIKE '#%' THEN lower(t.Name) LIKE '%' || substr(@Query, 2) || '%' " +
                                          "ELSE lower(b.Author) LIKE '%' || @Query || '%' OR lower(b.Title) LIKE '%' || @Query || '%' " +
                                          "END";
    }
}