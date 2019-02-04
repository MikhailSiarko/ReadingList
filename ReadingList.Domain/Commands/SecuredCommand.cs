using MediatR;

namespace ReadingList.Domain.Commands
{
    public abstract class SecuredCommand : IRequest
    {
        public readonly int UserId;

        protected SecuredCommand(int userId)
        {
            UserId = userId;
        }
    }

    public abstract class SecuredCommand<TEntity> : IRequest<TEntity>
    {
        public readonly int UserId;

        protected SecuredCommand(int userId)
        {
            UserId = userId;
        }
    }
}