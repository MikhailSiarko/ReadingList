namespace ReadingList.Application.Queries
{
    public interface IQuery
    {
        void InitializeSqlQueryContext(string sql);
    }
}