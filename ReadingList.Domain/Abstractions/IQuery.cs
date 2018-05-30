using MediatR;
using ReadingList.Domain.Implementations;

namespace ReadingList.Domain.Abstractions
{
    public interface IQuery<TResult> : IRequest<QueryResult<TResult>>
    { 
    }
}