using ReadingList.Application.DTO.User;

namespace ReadingList.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationData Authenticate(UserDto user);
    }
}