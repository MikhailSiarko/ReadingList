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
}