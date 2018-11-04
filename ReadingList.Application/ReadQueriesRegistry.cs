using System;
using System.Collections.Generic;
using ReadingList.Read;

namespace ReadingList.Application
{
    public class ReadQueriesRegistry : IReadQueriesRegistry
    {
        private readonly IReadOnlyDictionary<Type, string> _map;

        public ReadQueriesRegistry(IReadOnlyDictionary<Type, string> map)
        {
            _map = map;
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