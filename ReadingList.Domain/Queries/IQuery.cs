using MediatR;

namespace ReadingList.Domain.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    { 
    }
}