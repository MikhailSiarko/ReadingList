using ReadingList.Domain.DTO.User;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.Services.Authentication
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