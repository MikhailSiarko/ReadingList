using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Application.Queries;
using ReadingList.Read;

namespace ReadingList.Application.Infrastructure.Behaviors
{
    public class QueryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IReadQueriesRegistry _queriesRegistry;

        public QueryBehavior(IReadQueriesRegistry queriesRegistry)
        {
            _queriesRegistry = queriesRegistry;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!(request is IQuery query)) return next();
            
            if (_queriesRegistry.TryGetSql<TRequest>(out var sql))
            {
                query.InitializeSqlQueryContext(sql);
            }

            return next();
        }
    }
}