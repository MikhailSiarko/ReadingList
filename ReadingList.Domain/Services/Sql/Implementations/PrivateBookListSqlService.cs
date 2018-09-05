﻿using Cinch.SqlBuilder;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Services.Sql
{
    public class PrivateBookListSqlService : IBookListSqlService
    {
        public string GetBookListSqlQuery()
        {
            return new SqlBuilder().Select("l.Id", "l.Name", "l.OwnerId", "i.Id", "i.ReadingTimeInSeconds", "i.Title",
                    "i.Author", "i.Status")
                .From("BookLists AS l")
                .LeftJoin(
                    "(SELECT Id, Title, Author, BookListId, Status, ReadingTimeInSeconds FROM PrivateBookListItems) AS i ON i.BookListId = l.Id")
                .Where($"l.OwnerId = ({UserSqlService.GetUserIdSql}) AND l.Type = {BookListType.Private:D}")
                .ToSql();
        }

        public string GetBookListItemSqlQuery()
        {
            return new SqlBuilder()
                .Select("Id", "Title", "Author", "Status", "ReadingTimeInSeconds")
                .From("PrivateBookListItems")
                .Where(
                    $"BookListId = (SELECT Id FROM BookLists WHERE OwnerId = ({UserSqlService.GetUserIdSql}) AND Type = {BookListType.Private:D}) AND Title = @title AND Author = @author")
                .ToSql();
        }
    }
}