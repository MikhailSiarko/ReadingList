using System;
using MediatR;

namespace ReadingList.Application.Commands
{
    public abstract class SecuredCommand : IRequest
    {
        public readonly string UserLogin;

        protected SecuredCommand(string userLogin)
        {
            if(string.IsNullOrEmpty(userLogin))
                throw new ArgumentNullException(nameof(userLogin));
            
            UserLogin = userLogin;
        }
    }

    public abstract class SecuredCommand<TEntity> : IRequest<TEntity>
    {
        public readonly string UserLogin;

        protected SecuredCommand(string userLogin)
        {
            if (string.IsNullOrEmpty(userLogin))
                throw new ArgumentNullException(nameof(userLogin));

            UserLogin = userLogin;
        }
    }
}