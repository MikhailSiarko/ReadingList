using System;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Implementations;

namespace ReadingList.Domain.Absrtactions
{
    public abstract class CommandHandler<TCommand> : AsyncRequestHandler<TCommand, CommandResult>
        where TCommand : ICommand
    {
        protected sealed override async Task<CommandResult> HandleCore(TCommand request)
        {
            try
            {
                await Process(request);
                return CommandResult.Successed();
            }
            catch (Exception e)
            {
                return CommandResult.Failed(e.Message);
            }
        }

        protected abstract Task Process(TCommand command);
    }
}