using ReadingList.Models.Read;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationDataDto Authenticate(UserDto user);
    }
}