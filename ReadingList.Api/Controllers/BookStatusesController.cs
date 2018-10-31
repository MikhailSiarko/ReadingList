using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Application.Queries;
using ReadingList.Application.Services;

namespace ReadingList.Api.Controllers
{
    [ApiRoute("[controller]")]
    public class BookStatusesController : Controller
    {
        private readonly IApplicationService _domainService;

        public BookStatusesController(IApplicationService domainService)
        {
            _domainService = domainService;
        }
        
        public async Task<IActionResult> Get()
        {
            var statuses = await _domainService.AskAsync(new BookStatusesQuery());

            return Ok(statuses);
        }
    }
}