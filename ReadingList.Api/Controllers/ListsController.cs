using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Extensions;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.Controllers
{
    [ApiRoute("[controller]")]
    public class ListsController : Controller
    {
        private readonly IDomainService _domainService;

        public ListsController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpGet("moderated")]
        public async Task<IActionResult> GetAvailableForModeration()
        {
            var lists = await _domainService.AskAsync(new GetModeratedLists(User.GetUserId()));

            return Ok(lists);
        }
    }
}