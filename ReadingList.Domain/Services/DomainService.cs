using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.Services
{
    public class DomainService : IDomainService
    {
        private readonly IMediator _mediator;

        public DomainService(IMediator mediator)
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

        public async Task<TResult> AskAsync<TResult>(IRequest<TResult> query)
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

        public TResult Ask<TResult>(IRequest<TResult> query)
        {
            return _mediator.Send(query).RunSync();
        }
    }
}