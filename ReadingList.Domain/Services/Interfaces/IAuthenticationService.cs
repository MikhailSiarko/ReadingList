using ReadingList.Domain.Models.DTO;
using ReadingList.Domain.Models.DTO.User;
using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationDataDto Authenticate(UserDto user);
    }
}