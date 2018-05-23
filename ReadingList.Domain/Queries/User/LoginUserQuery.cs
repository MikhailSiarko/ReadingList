using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Domain.Queries
{
    public class LoginUserQuery : IQuery<AuthenticationData>
    {
        public readonly string Email;
        public readonly string Password;

        public LoginUserQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}