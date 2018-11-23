using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Api.RequestData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.Controllers
{
    [ApiRoute("[controller]")]
    public class AccountController : Controller
    {
        private readonly IDomainService _domainService;

        public AccountController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestData requestData)
        {
            var authenticationData =
                await _domainService.AskAsync(new LoginUserQuery(requestData.Email, requestData.Password));

            return Ok(authenticationData);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestData requestData)
        {
            await _domainService.ExecuteAsync(new RegisterUserCommand(requestData.Email, requestData.Password,
                requestData.ConfirmPassword));

            var authenticationData =
                await _domainService.AskAsync(new LoginUserQuery(requestData.Email, requestData.Password));

            return Ok(authenticationData);
        }
    }
}