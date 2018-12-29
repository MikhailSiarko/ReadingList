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
            [typeof(LoginUser)] = UserSqlQueries.SelectByLogin,
            [typeof(GetUser)] = UserSqlQueries.SelectById,
            [typeof(GetModerators)] = UserSqlQueries.SelectModerators,
            // Shared
            [typeof(FindSharedLists)] = SharedListSqlQueries.FindPreviews,
            [typeof(GetSharedList)] = SharedListSqlQueries.SelectById,
            [typeof(GetSharedListItems)] = SharedItemSqlQueries.SelectByListId,
            [typeof(GetSharedListItem)] = SharedItemSqlQueries.SelectById,
            [typeof(GetUserSharedLists)] = SharedListSqlQueries.SelectOwn,
            // Private
            [typeof(GetPrivateList)] = PrivateListSqlQueries.SelectByLogin,
            [typeof(GetPrivateListItem)] = PrivateItemSqlQueries.SelectById,
            // Book
            [typeof(FindBooks)] = BookSqlQueries.FindBooks,
            // Tags
            [typeof(GetTags)] = TagsSqlQueries.Select,
            // Lists
            [typeof(GetModeratedLists)] = ListsQueries.Select
        };
    }
}