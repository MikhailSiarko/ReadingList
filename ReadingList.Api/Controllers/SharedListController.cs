using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Filters;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [Route("api/list/shared")]
    public class SharedListController : Controller
    {
        private readonly IDomainService _domainService;

        public SharedListController(IDomainService domainService)
        {
            _domainService = domainService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var bookList = await _domainService.AskAsync(new GetSharedListQuery(id));

            return Ok(bookList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _domainService.ExecuteAsync(new DeleteSharedListCommand(User.Identity.Name, id));
            
            return Ok();
        }

        [HttpGet("own")]
        public async Task<IActionResult> Get()
        {
            var bookLists = await _domainService.AskAsync(new GetUserSharedListsQuery(User.Identity.Name));
            
            return Ok(bookLists);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CreateSharedListData sharedListData)
        {
            await _domainService.ExecuteAsync(new CreateSharedListCommand(User.Identity.Name, sharedListData.Name));

            return Ok();
        }

        [HttpPost("{listId}/items")]
        [ValidateModelState]
        public async Task<IActionResult> AddItem([FromRoute] int listId, [FromBody] AddItemToSharedListData itemToSharedListData)
        {
            await _domainService.ExecuteAsync(new AddSharedListItemCommand(listId,
                User.Identity.Name, itemToSharedListData.Title, itemToSharedListData.Author));

            return Ok();
        }

        [HttpDelete("{listId}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int listId, [FromRoute] int itemId)
        {
            await _domainService.ExecuteAsync(new DeleteSharedListItemCommand(User.Identity.Name, listId, itemId));
            
            return Ok();
        }

        [HttpPut("{listId}/items/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateItem([FromRoute] int listId, [FromRoute] int itemId, [FromBody] UpdateSharedListItemData itemData)
        {
            return Ok();
        }
    }
}