namespace ReadingList.Domain.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public readonly string Id;
        public readonly string Email;
        public readonly string Password;

        public RegisterUserCommand(string id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}