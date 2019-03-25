using System.Threading.Tasks;
using MediatR;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IDomainService
    {
        Task ExecuteAsync(IRequest command);

        Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> command);

        Task<TResult> AskAsync<TResult>(IRequest<TResult> query);

        void Execute(IRequest command);

        TResult Execute<TResult>(IRequest<TResult> command);

        TResult Ask<TResult>(IRequest<TResult> query);
    }
}