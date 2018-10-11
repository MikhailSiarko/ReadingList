using Cinch.SqlBuilder;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Services.Sql
{
    public class SharedBookListSqlService : ISharedBookListSqlService
    {
        public string GetBookListSqlQuery()
        {
            var getListsSql = new SqlBuilder()
                .Select("Id", "Name", "OwnerId", "Type")
                .From("BookLists")
                .Where($"Type = {BookListType.Shared:D}")
                .Where("Id = @id")
                .ToSql();
            
            var getTagsSql = new SqlBuilder()
                .Select("Name")
                .From("Tags")
                .Where("Id IN (SELECT TagId FROM SharedBookListTags WHERE SharedBookListId = @id)")
                .ToSql();

            return $"{getListsSql}; {getTagsSql}";
        }

        public string GetBookListItemSqlQuery()
        {
            var getItemsSql = new SqlBuilder()
                .Select("Id", "Author", "Title", "BookListId AS ListId", "(SELECT Name FROM Genres WHERE Id = GenreId) AS GenreId")
                .From("SharedBookListItems")
                .Where("BookListId = @listId")
                .Where("Id = @itemId")
                .ToSql();

            var getTagsSql = new SqlBuilder()
                .Select("Name")
                .From("Tags")
                .Where("Id IN (SELECT TagId FROM SharedBookListItemTags WHERE SharedBookListItemId = @itemId)")
                .ToSql();

            return $"{getItemsSql}; {getTagsSql}";
        }

        public string GetBookListsSqlQuery()
        {
            var getListsSql = new SqlBuilder()
                .Select("Id", "Name", "OwnerId", "Type")
                .From("BookLists")
                .Where($"Type = {BookListType.Shared:D}")
                .ToSql();
            
            var getTagsSql = new SqlBuilder()
                .Select("Name AS TagName", "SharedBookListId AS ListId")
                .From("Tags")
                .LeftJoin($"(SELECT TagId, SharedBookListId FROM SharedBookListTags WHERE SharedBookListId IN (SELECT Id FROM BookLists WHERE Type = {BookListType.Shared:D})) AS it ON it.TagId = Id")
                .ToSql();

            return $"{getListsSql}; {getTagsSql}";
        }

        public string GetUserBookListsSqlQuery()
        {
            var getListsSql = new SqlBuilder()
                .Select("Id", "Name", "OwnerId", "Type")
                .From("BookLists")
                .Where($"Type = {BookListType.Shared:D} AND OwnerId = ({UserSqlService.UserIdSql})")
                .ToSql();
            
            var getTagsSql = new SqlBuilder()
                .Select("Name AS TagName", "SharedBookListId AS ListId")
                .From("Tags")
                .LeftJoin($"(SELECT TagId, SharedBookListId FROM SharedBookListTags WHERE SharedBookListId IN (SELECT Id FROM BookLists WHERE Type = {BookListType.Shared:D} AND OwnerId = ({UserSqlService.UserIdSql}))) AS it ON it.TagId = Id")
                .ToSql();

            return $"{getListsSql}; {getTagsSql}";
        }

        public string GetSharedListItemsSqlQuery()
        {
            var getItemsSql = new SqlBuilder()
                .Select("Id", "Author", "Title", "BookListId AS ListId", "(SELECT Name FROM Genres WHERE Id = GenreId) AS GenreId")
                .From("SharedBookListItems")
                .Where("BookListId = @listId")
                .ToSql();

            var getTagsSql = new SqlBuilder()
                .Select("Name AS TagName", "SharedBookListItemId AS ItemId")
                .From("Tags")
                .LeftJoin("(SELECT TagId, SharedBookListItemId FROM SharedBookListItemTags WHERE SharedBookListItemId IN (SELECT Id FROM SharedBookListItems WHERE BookListId = @listId)) AS it ON it.TagId = Id")
                .ToSql();

            return $"{getItemsSql}; {getTagsSql}";
        }
    }
}