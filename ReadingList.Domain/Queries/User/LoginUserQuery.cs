using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Domain.Queries
{
    public class LoginUserQuery : IQuery<AuthenticationResult>
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