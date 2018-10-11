using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Commands;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class CommandHandler<TCommand> : AsyncRequestHandler<TCommand>
        where TCommand : ICommand
    {
        protected sealed override async Task HandleCore(TCommand request)
        {
            await Handle(request);
        }

        protected abstract Task Handle(TCommand command);
    }

    public abstract class CommandHandler<TCommand, TResult> : AsyncRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        protected sealed override async Task<TResult> HandleCore(TCommand request)
        {
            return await Handle(request);
        }

        protected abstract Task<TResult> Handle(TCommand command);
    }
}