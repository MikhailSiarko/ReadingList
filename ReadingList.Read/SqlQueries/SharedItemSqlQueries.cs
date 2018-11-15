using Cinch.SqlBuilder;

namespace ReadingList.Read.SqlQueries
{
    public static class SharedItemSqlQueries
    {
        public static string SelectById
        {
            get
            {
                var getItemSql = new SqlBuilder()
                    .Select("i.Id", "b.Author", "b.Title", "i.BookListId AS ListId", "b.GenreId", "i.BookId")
                    .From("SharedBookListItems AS i")
                    .LeftJoin("Books AS b ON b.Id = i.BookId")
                    .Where("BookListId = @ListId")
                    .Where("Id = @ItemId")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("t.Name AS TagName", "it.BookId")
                    .From("Tags AS t")
                    .LeftJoin("(" +
                              new SqlBuilder()
                                  .Select("bt.TagId", "bt.BookId")
                                  .From("BookTags AS bt")
                                  .Where("bt.BookId = (" +
                                         new SqlBuilder()
                                             .Select("i.BookId")
                                             .From("SharedBookListItems AS i")
                                             .Where("i.Id = @ItemId") + ")")
                                  .ToSql() +
                              ")" + " AS it ON it.TagId = t.Id)")
                    .ToSql();

                return $"{getItemSql}; {getTagsSql}";
            }
        }

        public static string SelectByListId
        {
            get
            {
                var getItemsSql = new SqlBuilder()
                    .Select("i.Id", "b.Author", "b.Title", "i.BookListId AS ListId", "b.GenreId", "i.BookId")
                    .From("SharedBookListItems AS i")
                    .LeftJoin("Books AS b ON b.Id = i.BookId")
                    .Where("BookListId = @ListId")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("t.Name AS TagName", "it.BookId")
                    .From("Tags AS t")
                    .LeftJoin("(" +
                              new SqlBuilder()
                                  .Select("bt.TagId", "bt.BookId")
                                  .From("BookTags AS bt")
                                  .Where("bt.BookId = (" +
                                         new SqlBuilder()
                                             .Select("i.BookId")
                                             .From("SharedBookListItems AS i")
                                             .Where("i.BookListId = @ListId") + ")")
                                  .ToSql() +
                              ")" + " AS it ON it.TagId = t.Id")
                    .ToSql();

                return $"{getItemsSql}; {getTagsSql}";
            }
        }
    }
}