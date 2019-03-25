using Cinch.SqlBuilder;

namespace ReadingList.Read.SqlQueries
{
    public static class TagsSqlQueries
    {
        public static string Select => new SqlBuilder()
            .Select("Id", "Name")
            .From("Tags")
            .ToSql();
    }
}