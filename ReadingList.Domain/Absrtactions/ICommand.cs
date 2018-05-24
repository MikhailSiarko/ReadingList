using MediatR;
using ReadingList.Domain.Implementations;

namespace ReadingList.Domain.Absrtactions
{
    public interface ICommand : IRequest<CommandResult>
    {
    }
}