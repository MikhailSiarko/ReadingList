using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.Services.Authentication
{
    public class AuthenticationData
    {
        public string Token { get; }
        public UserRm User { get; }

        public AuthenticationData(string token, UserRm user)
        {
            Token = token;
            User = user;
        }
    }
}