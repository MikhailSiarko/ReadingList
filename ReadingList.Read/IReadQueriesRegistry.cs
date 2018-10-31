using System;

namespace ReadingList.Read
{
    public interface IReadQueriesRegistry
    {
        bool TryGetSql<T>(out string sql);

        bool TryGetSql(Type type, out string sql);
    }
}