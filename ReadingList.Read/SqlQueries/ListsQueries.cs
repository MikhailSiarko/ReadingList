using Cinch.SqlBuilder;

namespace ReadingList.Read.SqlQueries
{
    public static class ListsQueries
    {
        public static string Select => new SqlBuilder()
            .Select("l.Id", "l.Name", "l.OwnerId", "l.Type", "(SELECT Login FROM Users WHERE Id = l.OwnerId) AS OwnerLogin")
            .From("BookLists AS l")
            .WhereOr("(" +
                     new SqlBuilder()
                         .Select("COUNT(bm.BookListId)")
                         .From("BookListModerators AS bm")
                         .Where("bm.BookListId = l.Id")
                         .Where("bm.UserId = @UserId")
                         .ToSql() +
                     ") = 1")
            .Where("l.OwnerId = @UserId")
            .ToSql();
    }
}