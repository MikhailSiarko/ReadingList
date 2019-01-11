using ReadingList.Models.Read;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationDataDto Authenticate(User user);
    }
}