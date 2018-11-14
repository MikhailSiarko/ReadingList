using System.Data;
using System.Threading.Tasks;
using MediatR;

namespace ReadingList.Read.QueryHandlers
{
    public abstract class QueryHandler<TQuery, TResult> : AsyncRequestHandler<TQuery, TResult> 
        where TQuery : class, IRequest<TResult>
    {
        protected readonly IDbConnection DbConnection;
        
        protected QueryHandler(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }
        
        protected sealed override async Task<TResult> HandleCore(TQuery request)
        {
            return await Handle(new SqlQueryContext<TQuery, TResult>(request));
        }

        protected abstract Task<TResult> Handle(SqlQueryContext<TQuery, TResult> context);
    }
}