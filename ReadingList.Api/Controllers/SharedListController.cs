using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Extensions;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Api.RequestData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [ListsRoute("shared")]
    public class SharedListController : Controller
    {
        private readonly IDomainService _domainService;

        public SharedListController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _domainService.ExecuteAsync(new DeleteSharedList(User.Claims.GetUserId(), id));

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query)
        {
            var bookLists = await _domainService.AskAsync(new FindSharedLists(query));

            return Ok(bookLists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var list = await _domainService.AskAsync(new GetSharedList(id, User.Claims.GetUserId()));

            return Ok(list);
        }

        [HttpGet("own")]
        public async Task<IActionResult> Get()
        {
            var bookLists = await _domainService.AskAsync(new GetUserSharedLists(User.Claims.GetUserId()));

            return Ok(bookLists);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SharedListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(new CreateSharedList(User.Claims.GetUserId(),
                requestData.Name, requestData.Tags));
            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SharedListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(
                new UpdateSharedList(User.Claims.GetUserId(), id, requestData.Name, requestData.Tags,
                    requestData.Moderators));

            return Ok(list);
        }

        [HttpGet("{listId}/items")]
        public async Task<IActionResult> GetItems([FromRoute] int listId)
        {
            var items = await _domainService.AskAsync(new GetSharedListItems(listId));

            return Ok(items);
        }

        [HttpPost("{listId}/items")]
        public async Task<IActionResult> AddItem([FromRoute] int listId, [FromBody] AddItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new AddSharedListItem(listId, User.Claims.GetUserId(),
                requestData.BookId));

            return Ok(item);
        }

        [HttpGet("{listId}/items/{itemId}")]
        public async Task<IActionResult> GetItem([FromRoute] int listId, [FromRoute] int itemId)
        {
            var item = await _domainService.AskAsync(new GetSharedListItem(listId, itemId));

            return Ok(item);
        }

        [HttpDelete("{listId}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int listId, [FromRoute] int itemId)
        {
            await _domainService.ExecuteAsync(new DeleteSharedListItem(User.Claims.GetUserId(), listId, itemId));

            return Ok();
        }

        [HttpPut("{listId}/items/{itemId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int listId, [FromRoute] int itemId,
            [FromBody] AddItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new UpdateSharedListItem(User.Claims.GetUserId(),
                itemId,
                listId));

            return Ok(item);
        }
    }
}