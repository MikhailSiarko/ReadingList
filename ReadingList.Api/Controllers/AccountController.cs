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
            var authenticationData = await AskAsync(new LoginUserQuery(loginData.Email, loginData.Password));
            
            return Ok(authenticationData);
        }

        [HttpPost]
        [Route("register")]
        [ValidateModelState]
        public async Task<IActionResult> Register([FromBody] RegisterData registerData)
        {
            await ExecuteAsync(new RegisterUserCommand(registerData.Email, registerData.Password));
            
            var authenticationData = await AskAsync(new LoginUserQuery(registerData.Email, registerData.Password));
            
            return Ok(authenticationData);
        }
    }
}