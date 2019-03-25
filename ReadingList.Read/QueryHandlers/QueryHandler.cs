using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ReadingList.Read.QueryHandlers
{
    public abstract class QueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : class, IRequest<TResult>
    {
        protected readonly IDbConnection DbConnection;

        protected QueryHandler(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await Handle(new SqlQueryContext<TQuery, TResult>(request));
        }

        protected abstract Task<TResult> Handle(SqlQueryContext<TQuery, TResult> context);
    }
}