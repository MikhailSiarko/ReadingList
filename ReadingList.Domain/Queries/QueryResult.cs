using ReadingList.Domain.Commands;

namespace ReadingList.Domain.Queries
{
    public class QueryResult<T> : CommandResult
    {
        public T Data { get; }
        
        public static QueryResult<T> Succeed(T data) => new QueryResult<T>(true, data);
        public new static QueryResult<T> Failed(string errorMessage) => new QueryResult<T>(false, errorMessage);

        private QueryResult(bool isSucceed, T data) : base(isSucceed)
        {
            Data = data;
        }

        private QueryResult(bool isSucceed, string errorMessage) : base(isSucceed, errorMessage)
        {
        }
    }
}