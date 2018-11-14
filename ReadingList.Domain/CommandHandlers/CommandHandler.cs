using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class CommandHandler<TCommand> : AsyncRequestHandler<TCommand>
        where TCommand : IRequest
    {
        protected readonly IDataStorage WriteService;

        protected CommandHandler(IDataStorage writeService)
        {
            WriteService = writeService;
        }

        protected sealed override async Task HandleCore(TCommand request)
        {
            await Handle(request);
        }

        protected abstract Task Handle(TCommand command);
    }

    public abstract class CommandHandler<TCommand, TResult> : AsyncRequestHandler<TCommand, TResult>
        where TCommand : IRequest<TResult>
    {
        protected readonly IDataStorage WriteService;

        protected CommandHandler(IDataStorage writeService)
        {
            WriteService = writeService;
        }

        protected sealed override async Task<TResult> HandleCore(TCommand request)
        {
            return await Handle(request);
        }

        protected abstract Task<TResult> Handle(TCommand command);
    }
}