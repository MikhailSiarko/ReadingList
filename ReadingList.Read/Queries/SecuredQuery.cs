using MediatR;

namespace ReadingList.Read.Queries
{
    public abstract class SecuredQuery<T> : IRequest<T>
    {
        public readonly int UserId;

        protected SecuredQuery(int userId)
        {
            UserId = userId;
        }
    }
}