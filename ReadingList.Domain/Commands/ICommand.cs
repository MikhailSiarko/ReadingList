using MediatR;

namespace ReadingList.Domain.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResut> : IRequest<TResut>
    {
    }
}