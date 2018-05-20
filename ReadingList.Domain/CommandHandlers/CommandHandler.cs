using System;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Commands;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class CommandHandler<TCommand> : AsyncRequestHandler<TCommand, CommandResult>
        where TCommand : ICommand
    {
        protected sealed override Task<CommandResult> HandleCore(TCommand request)
        {
            return Task.Run(async () =>
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
                
            });
        }

        protected abstract Task Process(TCommand command);
    }
}