using System;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Implementations;

namespace ReadingList.Domain.Abstractions
{
    public abstract class QueryHandler<TQuery, TResult> : AsyncRequestHandler<TQuery, QueryResult<TResult>> 
        where TQuery : IQuery<TResult>
    {
        protected sealed override async Task<QueryResult<TResult>> HandleCore(TQuery request)
        {
            try
            {
                var result = await Handle(request);
                return QueryResult<TResult>.Succeed(result);
            }
            catch (Exception e)
            {
                return QueryResult<TResult>.Failed(e.Message);
            }
        }

        protected abstract Task<TResult> Handle(TQuery query);
    }
}