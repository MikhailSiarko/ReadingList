using Cinch.SqlBuilder;

namespace ReadingList.Read.SqlQueries
{
    public static class TagsSqlQueries
    {
        public static string Select => new SqlBuilder()
            .Select("Id AS Value", "Name AS Text")
            .From("Tags")
            .ToSql();
    }
}