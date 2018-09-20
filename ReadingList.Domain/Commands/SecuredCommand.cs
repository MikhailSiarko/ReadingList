using System;

namespace ReadingList.Domain.Commands
{
    public abstract class SecuredCommand : ICommand
    {
        public readonly string UserLogin;

        protected SecuredCommand(string userLogin)
        {
            if(string.IsNullOrEmpty(userLogin))
                throw new ArgumentNullException(nameof(userLogin));
            
            UserLogin = userLogin;
        }
    }
}