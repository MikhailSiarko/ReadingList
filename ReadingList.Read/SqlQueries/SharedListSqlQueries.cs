using Cinch.SqlBuilder;
using ReadingList.Domain.Models.DAO;

namespace ReadingList.Read.SqlQueries
{
    public static class SharedListSqlQueries
    {
        public static string SelectById
        {
            get
            {
                const string canEditSql = "SELECT CASE " +
                                          "WHEN (SELECT COUNT(*) FROM BookLists WHERE Id = @ListId AND OwnerId = @UserId) = 1 THEN 1 " +
                                          "WHEN (SELECT COUNT(*) FROM BookListModerators WHERE BookListId = @ListId AND UserId = @UserId) = 1 THEN 1 " +
                                          "ELSE 0 " +
                                          "END";
                
                var getListsSql = new SqlBuilder()
                    .Select("Id", "Name", "OwnerId", "Type", $"({canEditSql}) AS Editable")
                    .From("BookLists")
                    .Where($"Type = {BookListType.Shared:D}")
                    .Where("Id = @ListId")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Name")
                    .From("Tags")
                    .Where("Id IN (" +
                           new SqlBuilder()
                               .Select("TagId")
                               .From("SharedBookListTags")
                               .Where("SharedBookListId = @ListId")
                               .ToSql() +
                           ")")
                    .ToSql();

                var getItemsSql = SharedItemSqlQueries.SelectByListId;   

                return $"{getListsSql}; {getTagsSql}; {getItemsSql};";
            }
        }

        public static string SelectPreviews
        {
            get
            {
                var getListsSql = new SqlBuilder()
                    .Select("l.Id", "l.Name", "l.OwnerId", "l.Type", "COUNT(i.Id) AS BooksCount")
                    .From("BookLists AS l")
                    .LeftJoin("SharedBookListItems AS i ON i.BookListId = l.Id")
                    .Where($"Type = {BookListType.Shared:D}")
                    .GroupBy("l.Id")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Name AS TagName", "SharedBookListId AS ListId")
                    .From("Tags")
                    .LeftJoin("(" +
                              new SqlBuilder()
                                  .Select("TagId", "SharedBookListId")
                                  .From("SharedBookListTags")
                                  .Where("SharedBookListId IN (" +
                                         new SqlBuilder()
                                             .Select("Id")
                                             .From("BookLists")
                                             .Where($"Type = {BookListType.Shared:D}")
                                             .ToSql() + ")") +
                              ") AS it ON it.TagId = Id")
                    .ToSql();

                return $"{getListsSql}; {getTagsSql}";
            }
        }
        
        public static string FindPreviews =>
            new SqlBuilder()
                .Select("DISTINCT l.Id", "l.Name", "l.Type", "l.OwnerId", "t.Name AS Tag", "COUNT(s.Id) AS BookCount")
                .From("BookLists AS l")
                .LeftJoin("SharedBookListTags AS lt on l.Id = lt.SharedBookListId")
                .LeftJoin("Tags AS t on lt.TagId = t.Id")
                .LeftJoin("SharedBookListItems s on l.Id = s.BookListId")
                .Where("l.Type = 2")
                .Where("CASE " +
                       "WHEN @Query IS NULL OR @Query = '' THEN (1 + 1)" +
                       "WHEN @Query LIKE '#%' THEN lower(t.Name) LIKE '%' || substr(@Query, 2) || '%' " +
                       "ELSE lower(l.Name) LIKE '%' || @Query || '%' " +
                       "END")
                .GroupBy("l.Id", "Tag")
                .ToSql();

        public static string SelectOwn
        {
            get
            {
                var getListsSql = new SqlBuilder()
                    .Select("l.Id", "l.Name", "l.OwnerId", "l.Type", "COUNT(i.Id) AS BooksCount")
                    .From("BookLists AS l")
                    .LeftJoin("SharedBookListItems AS i ON i.BookListId = l.Id")
                    .Where($"Type = {BookListType.Shared:D} AND OwnerId = @UserId")
                    .GroupBy("l.Id")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Name AS TagName", "SharedBookListId AS ListId")
                    .From("Tags")
                    .LeftJoin("(" +
                              new SqlBuilder()
                                  .Select("TagId", "SharedBookListId")
                                  .From("SharedBookListTags")
                                  .Where("SharedBookListId IN (" +
                                         new SqlBuilder()
                                             .Select("Id")
                                             .From("BookLists")
                                             .Where($"Type = {BookListType.Shared:D}")
                                             .Where("OwnerId = @UserId")
                                             .ToSql() + ")") +
                              ") AS it ON it.TagId = Id")
                    .ToSql();

                return $"{getListsSql}; {getTagsSql}";
            }
        }
    }
}