namespace ReadingList.Domain.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public readonly string Email;
        
        public readonly string Password;

        public RegisterUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}