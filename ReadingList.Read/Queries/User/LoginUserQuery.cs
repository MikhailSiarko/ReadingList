using MediatR;
using ReadingList.Domain.Models.DTO;
using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Read.Queries
{
    public class LoginUserQuery : IRequest<AuthenticationDataDto>
    {
        public readonly string Login;

        public readonly string Password;

        public LoginUserQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}