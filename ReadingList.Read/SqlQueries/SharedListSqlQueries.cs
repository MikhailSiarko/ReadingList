using Cinch.SqlBuilder;
using ReadingList.Models.Write;

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
                                          "ELSE 0 " +
                                          "END";

                const string canModerate = "SELECT CASE " +
                                           "WHEN (SELECT COUNT(*) FROM BookLists WHERE Id = @ListId AND OwnerId = @UserId) = 1 THEN 1 " +
                                           "WHEN (SELECT COUNT(*) FROM BookListModerators WHERE BookListId = @ListId AND UserId = @UserId) = 1 THEN 1 " +
                                           "ELSE 0 " +
                                           "END";

                var getListsSql = new SqlBuilder()
                    .Select("Id", "Name", "OwnerId", "Type", $"({canEditSql}) AS Editable", $"({canModerate}) AS CanBeModerated")
                    .From("BookLists")
                    .Where($"Type = {BookListType.Shared:D}")
                    .Where("Id = @ListId")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Id", "Name")
                    .From("Tags")
                    .Where("Id IN (" +
                           new SqlBuilder()
                               .Select("TagId")
                               .From("SharedBookListTags")
                               .Where("SharedBookListId = @ListId")
                               .ToSql() +
                           ")")
                    .ToSql();

                var getModerators = new SqlBuilder()
                    .Select("u.Id", "u.Login")
                    .From("BookListModerators")
                    .LeftJoin("Users AS u ON u.Id = UserId")
                    .Where("BookListId = @ListId");

                var getItemsSql = SharedItemSqlQueries.SelectByListId;

                return $"{getListsSql}; {getTagsSql}; {getItemsSql}; {getModerators};";
            }
        }

        public static string FindPreviews =>
            new SqlBuilder()
                .Select("DISTINCT l.Id", "l.Name", "l.Type", "l.OwnerId",
                    "(" +
                    new SqlBuilder()
                        .Select("GROUP_CONCAT(Name)")
                        .From("Tags")
                        .LeftJoin("SharedBookListTags ON Tags.Id = SharedBookListTags.TagId")
                        .Where("SharedBookListId = l.Id")
                        .ToSql() +
                    ") AS Tags", "COUNT(DISTINCT s.Id) AS BookCount")
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
                .GroupBy("l.Id")
                .OrderBy("l.Id " +
                         "LIMIT @Count + 1 " +
                         "OFFSET (@Count * (@Chunk - 1))")
                .ToSql();

        public static string SelectOwn =>
            new SqlBuilder()
                .Select("DISTINCT l.Id", "l.Name", "l.Type", "l.OwnerId",
                    "(" +
                    new SqlBuilder()
                        .Select("GROUP_CONCAT(Name)")
                        .From("Tags")
                        .LeftJoin("SharedBookListTags ON Tags.Id = SharedBookListTags.TagId")
                        .Where("SharedBookListId = l.Id")
                        .ToSql() +
                    ") AS Tags", "COUNT(DISTINCT s.Id) AS BookCount, (COUNT(DISTINCT l.Id) * 1.0 / @Count) AS Chunks")
                .From("BookLists AS l")
                .LeftJoin("SharedBookListTags AS lt on l.Id = lt.SharedBookListId")
                .LeftJoin("Tags AS t on lt.TagId = t.Id")
                .LeftJoin("SharedBookListItems s on l.Id = s.BookListId")
                .Where("l.Type = 2")
                .Where("l.OwnerId = @UserId")
                .GroupBy("l.Id")
                .OrderBy("l.Id " +
                         "LIMIT @Count + 1 " +
                         "OFFSET (@Count * (@Chunk - 1))")
                .ToSql();
    }
}