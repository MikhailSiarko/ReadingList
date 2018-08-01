using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Filters;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services;

namespace ReadingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ValidateModelState]
    public class AccountController : Controller
    {
        private readonly IDomainService _domainService;
        
        public AccountController(IDomainService domainService)
        {
            _domainService = domainService;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            return await AuthenticateUser(loginData.Email, loginData.Password);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterData registerData)
        {
            await _domainService.ExecuteAsync(new RegisterUserCommand(registerData.Email, registerData.Password));

            return await AuthenticateUser(registerData.Email, registerData.Password);
        }

        private async Task<IActionResult> AuthenticateUser(string email, string password)
        {
            var authenticationData = await _domainService.AskAsync(new LoginUserQuery(email, password));
            
            return Ok(authenticationData);
        }        
    }
}