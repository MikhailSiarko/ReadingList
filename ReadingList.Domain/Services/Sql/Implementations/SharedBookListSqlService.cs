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
            return new SqlBuilder()
                .Select("Id", "Title", "Author")
                .From("SharedBookListItems")
                .Where("Id = @id AND Title = @title AND Author = @author")
                .ToSql();
        }

        public string GetListsSqlQuery()
        {
            return new SqlBuilder()
                .Select("Id", "Name", "OwnerId", "JsonFields")
                .From("BookLists")
                .Where($"Type = {BookListType.Shared:D} AND OwnerId = (SELECT Id FROM Users WHERE Login = @login)")
                .ToSql();
        }
    }
}