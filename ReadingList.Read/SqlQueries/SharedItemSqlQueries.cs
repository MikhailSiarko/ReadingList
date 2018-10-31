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
                    .Select("Id", "Author", "Title", "BookListId AS ListId", "GenreId")
                    .From("SharedBookListItems")
                    .Where("BookListId = @ListId")
                    .Where("Id = @ItemId")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Name AS TagName", "SharedBookListItemId AS ItemId")
                    .From("Tags")
                    .LeftJoin("(SELECT TagId, SharedBookListItemId FROM SharedBookListItemTags WHERE SharedBookListItemId = @ItemId) AS it ON it.TagId = Id")
                    .ToSql();

                return $"{getItemSql}; {getTagsSql}";
            }
        }

        public static string SelectByListId
        {
            get
            {
                var getItemsSql = new SqlBuilder()
                    .Select("Id", "Author", "Title", "BookListId AS ListId", "GenreId")
                    .From("SharedBookListItems")
                    .Where("BookListId = @ListId")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Name AS TagName", "SharedBookListItemId AS ItemId")
                    .From("Tags")
                    .LeftJoin("(SELECT TagId, SharedBookListItemId FROM SharedBookListItemTags WHERE SharedBookListItemId IN (SELECT Id FROM SharedBookListItems WHERE BookListId = @ListId)) AS it ON it.TagId = Id")
                    .ToSql();

                return $"{getItemsSql}; {getTagsSql}";
            }
        }
    }
}