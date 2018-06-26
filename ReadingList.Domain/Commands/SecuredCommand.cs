namespace ReadingList.Domain.Commands
{
    public abstract class SecuredCommand : ICommand
    {
        public readonly string UserLogin;

        protected SecuredCommand(string userLogin)
        {
            UserLogin = userLogin;
        }
    }
}