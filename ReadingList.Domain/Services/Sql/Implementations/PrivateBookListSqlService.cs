using Cinch.SqlBuilder;
using ReadingList.Domain.Services.Sql.Interfaces;

namespace ReadingList.Domain.Services.Sql
{
    public class PrivateBookListSqlService : IPrivateBookListSqlService
    {
        public string GetPrivateBookListSqlQuery()
        {
            return new SqlBuilder().Select("l.Id", "l.Name", "l.OwnerId", "i.Id", "i.ReadingTime", "i.Title",
                    "i.Author", "i.Status")
                .From("BookLists AS l")
                .LeftJoin(
                    "(SELECT Id, Title, Author, BookListId, Status, ReadingTime FROM PrivateBookListItems) AS i ON i.BookListId = l.Id")
                .Where("l.OwnerId = (SELECT Id FROM Users WHERE Login = @login)")
                .Where("l.Type = @type")
                .ToSql();
        }

        public string GetPrivateBookListItemSqlQuery()
        {
            return new SqlBuilder()
                .Select("Id", "Title", "Author", "Status", "ReadingTime")
                .From("PrivateBookListItems")
                .Where(
                    "BookListId = (SELECT Id From BookLists WHERE OwnerId = (SELECT Id FROM Users WHERE Login = @login) AND Type = @type) AND Title = @title AND Author = @author")
                .ToSql();
        }
    }
}