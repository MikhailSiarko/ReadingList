using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Queries;

namespace ReadingList.Domain.Services
{
    public class DomainService : IDomainService
    {
        private readonly IMediator _mediator;

        public DomainService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ExecuteAsync(ICommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command)
        {
            return await _mediator.Send(command);
        }

        public async Task<TResult> AskAsync<TResult>(IQuery<TResult> query)
        {
            return await _mediator.Send(query);
        }
        
        public void Execute(ICommand command)
        {
            _mediator.Send(command).RunSync();
        }

        public TResult Execute<TResult>(ICommand<TResult> command)
        {
            return _mediator.Send(command).RunSync();
        }

        public TResult Ask<TResult>(IQuery<TResult> query)
        {
            return _mediator.Send(query).RunSync();
        }
    }
}