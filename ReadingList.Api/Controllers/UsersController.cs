using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Extensions;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [ApiRoute("[controller]")]
    public class UsersController : Controller
    {
        private readonly IDomainService _domainService;

        public UsersController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task<IActionResult> Get()
        {
            var users = await _domainService.AskAsync(new GetModerators(User.GetUserId()));

            return Ok(users);
        }
    }
}