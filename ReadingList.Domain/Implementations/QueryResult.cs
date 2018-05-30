using ReadingList.Domain.Abstractions;

namespace ReadingList.Domain.Implementations
{
    public class QueryResult<T> : Result<T>
    { 
        public static QueryResult<T> Succeed(T data) => new QueryResult<T>(true, data);
        public static QueryResult<T> Failed(string errorMessage) => new QueryResult<T>(false, errorMessage);
        
        private QueryResult(bool isSucceed, T data) : base(isSucceed, data)
        {
        }

        private QueryResult(bool isSucceed, string errorMessage) : base(isSucceed, errorMessage)
        {
        }
    }
}