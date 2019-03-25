namespace ReadingList.Models.Read
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