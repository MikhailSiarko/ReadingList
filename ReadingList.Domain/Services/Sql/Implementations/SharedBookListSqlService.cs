using Cinch.SqlBuilder;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Services.Sql
{
    public class SharedBookListSqlService : ISharedBookListSqlService
    {
        public string GetBookListSqlQuery()
        {
            return new SqlBuilder()
                .Select("l.Id", "l.Name", "l.OwnerId", "l.JsonFields", "i.Id", "i.Title", "i.Author")
                .From("BookLists AS l")
                .LeftJoin("(SELECT Id, Title, Author, BookListId FROM SharedBookListItems) AS i ON i.BookListId = l.Id")
                .Where($"l.Type = {BookListType.Shared:D} AND l.Id = @id")
                .ToSql();
        }

        public string GetBookListItemSqlQuery()
        {
            const string getItemsSql =
                "SELECT Id, Author, Title, BookListId AS ListId, (SELECT Name FROM Categories WHERE Id = CategoryId) AS Category FROM SharedBookListItems WHERE BookListId = @listId AND Id = @itemId";

            const string getTagsSql =
                "select Name from Tags where Id in (select TagId from SharedBookListItemTags where SharedBookListItemId = @itemId)";

            return $"{getItemsSql} {getTagsSql}";
        }

        public string GetBookListsSqlQuery()
        {
            return new SqlBuilder()
                .Select("Id", "Name", "OwnerId", "JsonFields")
                .From("BookLists")
                .Where($"Type = {BookListType.Shared:D}")
                .ToSql();
        }

        public string GetUserBookListsSqlQuery()
        {
            return new SqlBuilder()
                .Select("Id", "Name", "OwnerId", "JsonFields")
                .From("BookLists")
                .Where($"Type = {BookListType.Shared:D} AND OwnerId = ({UserSqlService.UserIdSql})")
                .ToSql();
        }

        public string GetSharedListItemsSqlQuery()
        {
            const string getItemsSql =
                "SELECT Id, Author, Title, BookListId AS ListId, (SELECT Name FROM Categories WHERE Id = CategoryId) AS Category FROM SharedBookListItems WHERE BookListId = @listId";

            const string getTagsSql =
                "SELECT Name as TagName, SharedBookListItemId as ItemId FROM Tags LEFT JOIN (SELECT TagId, SharedBookListItemId FROM SharedBookListItemTags WHERE SharedBookListItemId IN (SELECT Id FROM SharedBookListItems WHERE BookListId = @listId)) AS it ON it.TagId = Id";

            return $"{getItemsSql} {getTagsSql}";
        }
    }
}