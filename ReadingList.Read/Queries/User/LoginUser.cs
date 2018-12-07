using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class LoginUser : IRequest<AuthenticationDataDto>
    {
        public readonly string Login;

        public readonly string Password;

        public LoginUser(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}