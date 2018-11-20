using ReadingList.Domain.Models.DTO.User;

namespace ReadingList.Domain.Models.DTO
{
    public class AuthenticationDataDto
    {
        public string Token { get; }

        public UserIdentityDto User { get; }

        public AuthenticationDataDto(string token, UserIdentityDto user)
        {
            Token = token;
            User = user;
        }
    }
}