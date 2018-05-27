using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Abstractions;
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
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await AskAsync(new LoginUserQuery(loginData.Email, loginData.Password));

            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterData registerData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result =
                await ExecuteAsync(new RegisterUserCommand(registerData.Email,
                    registerData.Password));

            if (!result.IsSucceed)
                return BadRequest(result);

            var queryResult = await AskAsync(new LoginUserQuery(registerData.Email, registerData.Password));
            
            if (!queryResult.IsSucceed)
                return StatusCode(500, queryResult);
            
            return Ok(queryResult);
        }
    }
}