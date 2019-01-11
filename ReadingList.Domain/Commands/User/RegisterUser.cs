using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class RegisterUser : IRequest<AuthenticationDataDto>
    {
        public readonly string Email;

        public readonly string Password;

        public readonly string ConfirmPassword;

        public RegisterUser(string email, string password, string confirmPassword)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}