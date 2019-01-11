using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class LoginUser : IRequest<AuthenticationDataDto>
    {
        public readonly string Email;

        public readonly string Password;

        public LoginUser(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}