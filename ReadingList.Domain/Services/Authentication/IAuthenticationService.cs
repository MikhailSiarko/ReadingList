using ReadingList.Domain.Queries;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Authenticate(User user, LoginUserQuery query);
    }
}