using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services;

namespace ReadingList.Api.Controllers
{
    [ApiRoute("[controller]")]
    public class BookStatusesController : Controller
    {
        private readonly IDomainService _domainService;

        public BookStatusesController(IDomainService domainService)
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