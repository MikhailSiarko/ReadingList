using System.Threading.Tasks;
using MediatR;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Queries;

namespace ReadingList.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMediator _mediator;

        public ApplicationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ExecuteAsync(IRequest command)
        {
            await _mediator.Send(command);
        }

        public async Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> command)
        {
            return await _mediator.Send(command);
        }

        public async Task<TResult> AskAsync<TResult>(Query<TResult> query)
        {
            return await _mediator.Send(query);
        }
        
        public void Execute(IRequest command)
        {
            _mediator.Send(command).RunSync();
        }

        public TResult Execute<TResult>(IRequest<TResult> command)
        {
            return _mediator.Send(command).RunSync();
        }

        public TResult Ask<TResult>(Query<TResult> query)
        {
            return _mediator.Send(query).RunSync();
        }
    }
}