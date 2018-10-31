using Cinch.SqlBuilder;
using ReadingList.Domain.Enumerations;

namespace ReadingList.Read.SqlQueries
{
    public static class SharedListSqlQueries
    {
        public static string SelectById
        {
            get
            {
                var getListsSql = new SqlBuilder()
                    .Select("Id", "Name", "OwnerId", "Type")
                    .From("BookLists")
                    .Where($"Type = {BookListType.Shared:D}")
                    .Where("Id = @ListId")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Name")
                    .From("Tags")
                    .Where("Id IN (SELECT TagId FROM SharedBookListTags WHERE SharedBookListId = @ListId)")
                    .ToSql();

                var getItemsSql = GetSharedListItemsSqlQuery();

                return $"{getListsSql}; {getTagsSql}; {getItemsSql}";
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
                    .LeftJoin($"(SELECT TagId, SharedBookListId FROM SharedBookListTags WHERE SharedBookListId IN (SELECT Id FROM BookLists WHERE Type = {BookListType.Shared:D})) AS it ON it.TagId = Id")
                    .ToSql();

                return $"{getListsSql}; {getTagsSql}";
            }
        }

        public static string SelectOwn
        {
            get
            {
                var getListsSql = new SqlBuilder()
                    .Select("l.Id", "l.Name", "l.OwnerId", "l.Type", "COUNT(i.Id) AS BooksCount")
                    .From("BookLists AS l")
                    .LeftJoin("SharedBookListItems AS i ON i.BookListId = l.Id")
                    .Where($"Type = {BookListType.Shared:D} AND OwnerId = ({UserSqlQueries.SelectIdByLogin})")
                    .GroupBy("l.Id")
                    .ToSql();

                var getTagsSql = new SqlBuilder()
                    .Select("Name AS TagName", "SharedBookListId AS ListId")
                    .From("Tags")
                    .LeftJoin($"(SELECT TagId, SharedBookListId FROM SharedBookListTags WHERE SharedBookListId IN (SELECT Id FROM BookLists WHERE Type = {BookListType.Shared:D} AND OwnerId = ({UserSqlQueries.SelectIdByLogin}))) AS it ON it.TagId = Id")
                    .ToSql();

                return $"{getListsSql}; {getTagsSql}";
            }
        }
        
        private static string GetSharedListItemsSqlQuery()
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