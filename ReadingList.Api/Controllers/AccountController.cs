using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Queries;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Queries;
using ReadingList.Domain.ReadModel;
using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await AskAsync(new LoginUserQuery(loginQuery.Email, loginQuery.Password));

            if (!result.IsSucceed)
                return BadRequest(result.ErrorMessage);
            
            var token = _authenticationService.EncodeSecurityToken(result.Data);
            var response = GenerateAuthResponse(token, result.Data);
            return Ok(response);
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

            var user = new UserRm
            {
                Id = registerQuery.Id,
                Email = registerQuery.Email
            };
            var token = _authenticationService.EncodeSecurityToken(user);
            var response = GenerateAuthResponse(token, user);
            return Ok(response);
        }
        
        private static dynamic GenerateAuthResponse(string token, UserRm user)
        {
            return new
            {
                token,
                user = new
                {
                    id = user.Id,
                    email = user.Email,
                    firstname = user.Firstname,
                    lastname = user.Lastname
                }
            };
        }
    }
}