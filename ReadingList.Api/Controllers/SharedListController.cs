using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Commands.SharedList;
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

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateSharedListData sharedListData)
        {
            await _domainService.ExecuteAsync(new CreateSharedListCommand(User.Identity.Name, sharedListData.Name));

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] AddItemToSharedListData itemToSharedListData)
        {
            await _domainService.ExecuteAsync(new AddSharedListItemCommand(itemToSharedListData.ListId,
                User.Identity.Name, itemToSharedListData.Title, itemToSharedListData.Author));

            return Ok();
        }
    }
}