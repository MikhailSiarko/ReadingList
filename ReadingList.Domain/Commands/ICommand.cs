using MediatR;

namespace ReadingList.Domain.Commands
{
    public interface ICommand : IRequest<CommandResult>
    {
    }
}