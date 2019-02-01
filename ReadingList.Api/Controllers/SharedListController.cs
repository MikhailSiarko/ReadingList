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
            await _domainService.ExecuteAsync(new DeleteSharedList(User.GetUserId(), id));

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRange([FromQuery] string query, [FromQuery] int? chunk, [FromQuery] int? count)
        {
            var bookLists = await _domainService.AskAsync(new FindSharedLists(query, chunk, count));

            return Ok(bookLists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var list = await _domainService.AskAsync(new GetSharedList(id, User.GetUserId()));

            return Ok(list);
        }

        [HttpGet("own")]
        public async Task<IActionResult> GetOwn([FromQuery] int? chunk, [FromQuery] int? count)
        {
            var bookLists = await _domainService.AskAsync(new GetUserSharedLists(User.GetUserId(), chunk, count));

            return Ok(bookLists);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SharedListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(new CreateSharedList(User.GetUserId(),
                requestData.Name, requestData.Tags));
            return CreatedAtAction(nameof(Get), new {id = list.Id}, list);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] SharedListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(
                new UpdateSharedList(User.GetUserId(), id, requestData.Name, requestData.Tags,
                    requestData.Moderators));

            return Ok(list);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItems([FromRoute] int id)
        {
            var items = await _domainService.AskAsync(new GetSharedListItems(id));

            return Ok(items);
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddItem([FromRoute] int id, [FromBody] AddItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new AddSharedListItem(id, User.GetUserId(),
                requestData.BookId));

            return CreatedAtAction(nameof(GetItem), new {id, itemId = item.Id}, item);
        }

        [HttpGet("{id}/items/{itemId}")]
        public async Task<IActionResult> GetItem([FromRoute] int id, [FromRoute] int itemId)
        {
            var item = await _domainService.AskAsync(new GetSharedListItem(id, itemId));

            return Ok(item);
        }

        [HttpDelete("{id}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id, [FromRoute] int itemId)
        {
            await _domainService.ExecuteAsync(new DeleteSharedListItem(User.GetUserId(), id, itemId));

            return Ok();
        }

        [HttpPatch("{id}/items/{itemId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromRoute] int itemId, [FromBody] AddItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new UpdateSharedListItem(User.GetUserId(), id, itemId));

            return Ok(item);
        }
    }
}