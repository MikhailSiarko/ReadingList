using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand>
        where TCommand : IRequest
    {
        protected readonly IDataStorage WriteService;

        protected CommandHandler(IDataStorage writeService)
        {
            WriteService = writeService;
        }

        public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
        {
            await Handle(command);
            return Unit.Value;
        }

        protected abstract Task Handle(TCommand command);
    }

    public abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : IRequest<TResult>
    {
        protected readonly IDataStorage WriteService;

        protected CommandHandler(IDataStorage writeService)
        {
            WriteService = writeService;
        }

        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            return await Handle(command);
        }

        protected abstract Task<TResult> Handle(TCommand command);
    }
}