using ReadingList.Application.DTO.User;

namespace ReadingList.Application.Services.Authentication
{
    public class AuthenticationData
    {
        public string Token { get; }

        public UserIdentityDto User { get; }

        public AuthenticationData(string token, UserIdentityDto user)
        {
            Token = token;
            User = user;
        }
    }
}