using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Queries;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Queries;

namespace ReadingList.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await AskAsync(new LoginUserQuery(loginQuery.Email, loginQuery.Password));

            if (!result.IsSucceed)
                return BadRequest(result.ErrorMessage);
            return Ok(result.Data);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterQuery registerQuery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result =
                await ExecuteAsync(new RegisterUserCommand(registerQuery.Id, registerQuery.Email,
                    registerQuery.Password));

            if (!result.IsSucceed)
                return BadRequest(result.ErrorMessage);

            var queryResult = await AskAsync(new LoginUserQuery(registerQuery.Email, registerQuery.Password));
            
            if (!queryResult.IsSucceed)
                return StatusCode(500, queryResult.ErrorMessage);
            
            return Ok(queryResult.Data);
        }
    }
}