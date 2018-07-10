using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Abstractions;
using ReadingList.Api.Filters;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Queries;

namespace ReadingList.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        [HttpPost]
        [Route("login")]
        [ValidateModelState]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            return await AuthenticateUser(loginData.Email, loginData.Password);
        }

        [HttpPost]
        [Route("register")]
        [ValidateModelState]
        public async Task<IActionResult> Register([FromBody] RegisterData registerData)
        {
            await ExecuteAsync(new RegisterUserCommand(registerData.Email, registerData.Password));

            return await AuthenticateUser(registerData.Email, registerData.Password);
        }

        private async Task<IActionResult> AuthenticateUser(string email, string password)
        {
            var authenticationData = await AskAsync(new LoginUserQuery(email, password));
            
            return Ok(authenticationData);
        }
    }
}