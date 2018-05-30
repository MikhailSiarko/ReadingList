using MediatR;
using ReadingList.Domain.Implementations;

namespace ReadingList.Domain.Abstractions
{
    public interface ICommand : IRequest<CommandResult>
    {
    }
}