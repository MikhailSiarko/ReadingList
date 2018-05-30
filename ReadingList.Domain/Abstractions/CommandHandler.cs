using System;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Implementations;

namespace ReadingList.Domain.Abstractions
{
    public abstract class CommandHandler<TCommand> : AsyncRequestHandler<TCommand, CommandResult>
        where TCommand : ICommand
    {
        protected sealed override async Task<CommandResult> HandleCore(TCommand request)
        {
            try
            {
                await Handle(request);
                return CommandResult.Successed();
            }
            catch (Exception e)
            {
                return CommandResult.Failed(e.Message);
            }
        }

        protected abstract Task Handle(TCommand command);
    }
}