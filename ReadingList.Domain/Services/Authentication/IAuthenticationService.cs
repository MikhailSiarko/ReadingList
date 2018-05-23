using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Services.Authentication
{
    public interface IAuthenticationService
    {
        string EncodeSecurityToken(User user);
        AuthenticationData GenerateAuthResponse(string token, User user);
    }
}