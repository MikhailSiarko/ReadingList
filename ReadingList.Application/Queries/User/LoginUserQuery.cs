using ReadingList.Application.Services.Authentication;

namespace ReadingList.Application.Queries
{
    public class LoginUserQuery : Query<AuthenticationData>
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