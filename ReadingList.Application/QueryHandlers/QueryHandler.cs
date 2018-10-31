using System.Threading.Tasks;
using MediatR;
using ReadingList.Read;

namespace ReadingList.Application.QueryHandlers
{
    public abstract class QueryHandler<TQuery, TResult> : AsyncRequestHandler<TQuery, TResult> 
        where TQuery : IRequest<TResult>
    {
        protected readonly IApplicationDbConnection DbConnection;
        
        protected QueryHandler(IApplicationDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }
        
        protected sealed override async Task<TResult> HandleCore(TQuery request)
        {
            return await Handle(request);
        }

        protected abstract Task<TResult> Handle(TQuery query);
    }
}