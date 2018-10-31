using System;
using System.Collections.Generic;
using ReadingList.Application.Queries;
using ReadingList.Application.Queries.SharedList;
using ReadingList.Read;
using ReadingList.Read.SqlQueries;

namespace ReadingList.Application
{
    public class ReadQueriesRegistry : IReadQueriesRegistry
    {
        private readonly IReadOnlyDictionary<Type, string> _map;

        public ReadQueriesRegistry()
        {
            _map = InitializeMap();
        }

        private static IReadOnlyDictionary<Type, string> InitializeMap()
        {
            return new Dictionary<Type, string>
            {
                // User
                [typeof(LoginUserQuery)] = UserSqlQueries.SelectByLogin,
                [typeof(GetUserQuery)] = UserSqlQueries.SelectById,
                // Shared
                [typeof(FindSharedListsQuery)] = SharedListSqlQueries.SelectPreviews, // TODO change after search logic implementation
                [typeof(GetSharedListQuery)] = SharedListSqlQueries.SelectById,
                [typeof(GetSharedListItemsQuery)] = SharedItemSqlQueries.SelectByListId,
                [typeof(GetSharedListItemQuery)] = SharedItemSqlQueries.SelectById,
                [typeof(GetUserSharedListsQuery)] = SharedListSqlQueries.SelectOwn,
                // Private
                [typeof(GetPrivateListQuery)] = PrivateListSqlQueries.SelectByLogin,
                [typeof(GetPrivateListItemQuery)] = PrivateItemSqlQueries.SelectById
            };
        }

        public bool TryGetSql<T>(out string sql)
        {
            return _map.TryGetValue(typeof(T), out sql);
        }

        public bool TryGetSql(Type type, out string sql)
        {
            return _map.TryGetValue(type, out sql);
        }
    }
}