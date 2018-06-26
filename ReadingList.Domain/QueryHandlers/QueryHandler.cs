using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Queries;

namespace ReadingList.Domain.QueryHandlers
{
    public abstract class QueryHandler<TQuery, TResult> : AsyncRequestHandler<TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {
        protected sealed override async Task<TResult> HandleCore(TQuery request)
        {
            return await Handle(request);
        }

        protected abstract Task<TResult> Handle(TQuery query);
    }
}