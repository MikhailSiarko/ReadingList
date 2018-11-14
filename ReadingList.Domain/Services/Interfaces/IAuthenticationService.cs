using ReadingList.Domain.Models.DTO.User;
using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationData Authenticate(UserDto user);
    }
}