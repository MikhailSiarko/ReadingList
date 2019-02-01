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
    public class BookStatusesController : Controller
    {
        private readonly IDomainService _domainService;

        public BookStatusesController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task<IActionResult> Get()
        {
            var statuses = await _domainService.AskAsync(new GetBookStatuses());

            return Ok(statuses);
        }
    }
}