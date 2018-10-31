using Dapper;

namespace ReadingList.Read
{
    public interface ISqlQueryContext
    {
        DynamicParameters QueryParameters { get; }

        string Sql { get; }
    }
}