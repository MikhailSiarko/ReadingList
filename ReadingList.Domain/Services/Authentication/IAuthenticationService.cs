using ReadingList.Domain.ReadModel;

namespace ReadingList.Domain.Services.Authentication
{
    public interface IAuthenticationService
    {
        string EncodeSecurityToken(UserRm userRm);
    }
}