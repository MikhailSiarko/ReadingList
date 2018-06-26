using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationData Authenticate(User user);
    }
}