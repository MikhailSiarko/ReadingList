using ReadingList.Domain.ReadModel;

namespace ReadingList.Domain.Queries
{
    public class LoginUserQuery : IQuery<UserRm>
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