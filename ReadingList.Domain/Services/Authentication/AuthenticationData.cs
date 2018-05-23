using UserRM = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.Services.Authentication
{
    public class AuthenticationData
    {
        public readonly string Token;
        public readonly UserRM User;

        public AuthenticationData(string token, UserRM user)
        {
            Token = token;
            User = user;
        }
    }
}