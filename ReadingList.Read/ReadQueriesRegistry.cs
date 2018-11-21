using System;
using System.Collections.Generic;
using ReadingList.Read.Queries;
using ReadingList.Read.Queries.Book;
using ReadingList.Read.SqlQueries;

namespace ReadingList.Read
{
    public static class ReadQueriesRegistry
    {
        public static bool TryGetSql<T>(out string sql)
        {
            return ReadQueriesMap.TryGetValue(typeof(T), out sql);
        }

        public static bool TryGetSql(Type type, out string sql)
        {
            return ReadQueriesMap.TryGetValue(type, out sql);
        }
        
        private static IReadOnlyDictionary<Type, string> ReadQueriesMap => new Dictionary<Type, string>
        {
            // User
            [typeof(LoginUserQuery)] = UserSqlQueries.SelectByLogin,
            [typeof(GetUserQuery)] = UserSqlQueries.SelectById,
            // Shared
            [typeof(FindSharedListsQuery)] = SharedListSqlQueries.FindPreviews,
            [typeof(GetSharedListQuery)] = SharedListSqlQueries.SelectById,
            [typeof(GetSharedListItemsQuery)] = SharedItemSqlQueries.SelectByListId,
            [typeof(GetSharedListItemQuery)] = SharedItemSqlQueries.SelectById,
            [typeof(GetUserSharedListsQuery)] = SharedListSqlQueries.SelectOwn,
            // Private
            [typeof(GetPrivateListQuery)] = PrivateListSqlQueries.SelectByLogin,
            [typeof(GetPrivateListItemQuery)] = PrivateItemSqlQueries.SelectById,
            // Book
            [typeof(FindBooksQuery)] = BookSqlQueries.FindBooks
        };
    }
}