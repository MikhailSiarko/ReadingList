using MediatR;

namespace ReadingList.Domain.Queries
{
    public interface IQuery<TResult> : IRequest<QueryResult<TResult>>
    { 
    }
}