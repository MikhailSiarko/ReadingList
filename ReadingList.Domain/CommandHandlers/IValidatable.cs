using ReadingList.Domain.Commands;

namespace ReadingList.Domain.CommandHandlers
{
    public interface IValidatable<in TCommand, in TEntity> where TCommand : ICommand
    {
        void Validate(TEntity entity, TCommand command);
    }
}