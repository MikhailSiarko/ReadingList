using System;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Queries;

namespace ReadingList.Domain.QueryHandlers
{
    public abstract class QueryHandler<TQuery, TResult> : AsyncRequestHandler<TQuery, QueryResult<TResult>> 
        where TQuery : IQuery<TResult>
    {
        protected sealed override Task<QueryResult<TResult>> HandleCore(TQuery request)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var result = await Process(request);
                    return QueryResult<TResult>.Succeed(result);
                }
                catch (Exception e)
                {
                    return QueryResult<TResult>.Failed(e.Message);
                }
                
            });
        }

        protected abstract Task<TResult> Process(TQuery query);
    }
}