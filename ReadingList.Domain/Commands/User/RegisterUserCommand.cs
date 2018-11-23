using MediatR;

namespace ReadingList.Domain.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public readonly string Email;

        public readonly string Password;

        public readonly string ConfirmPassword;

        public RegisterUserCommand(string email, string password, string confirmPassword)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}