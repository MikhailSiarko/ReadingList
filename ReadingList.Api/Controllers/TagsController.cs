using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [ApiRoute("[controller]")]
    public class TagsController : Controller
    {
        private readonly IDomainService _domainService;

        public TagsController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task<IActionResult> Get()
        {
            var tags = await _domainService.AskAsync(new GetTags());

            return Ok(tags);
        }
    }
}