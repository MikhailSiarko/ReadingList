using System.Threading.Tasks;
using MediatR;
using ReadingList.Application.Queries;

namespace ReadingList.Application.Services
{
    public interface IApplicationService
    {
        Task ExecuteAsync(IRequest command);

        Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> command);

        Task<TResult> AskAsync<TResult>(Query<TResult> query);

        void Execute(IRequest command);

        TResult Execute<TResult>(IRequest<TResult> command);

        TResult Ask<TResult>(Query<TResult> query);
    }
}