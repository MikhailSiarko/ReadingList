using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SharedListsController : Controller
    {
        private readonly IDomainService _domainService;

        public SharedListsController(IDomainService domainService)
        {
            _domainService = domainService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var bookList = await _domainService.AskAsync(new GetSharedListQuery(id));

            return Ok(bookList);
        }

        [HttpGet("own")]
        public async Task<IActionResult> GetUserSharedLists()
        {
            var bookLists = await _domainService.AskAsync(new GetSharedListsQuery(User.Identity.Name));
            
            return Ok(bookLists);
        }
    }
}