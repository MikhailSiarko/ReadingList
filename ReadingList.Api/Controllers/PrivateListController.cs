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
    [ListsRoute("private")]
    public class PrivateListController : Controller
    {
        private readonly IDomainService _domainService;

        public PrivateListController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bookList = await _domainService.AskAsync(new GetPrivateList(User.GetUserId()));

            return Ok(bookList);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] UpdatePrivateListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(new UpdatePrivateList(User.GetUserId(),
                requestData.Name));

            return Ok(list);
        }

        [HttpPost("share/{name}")]
        public async Task<IActionResult> Share([FromRoute] string name)
        {
            await _domainService.ExecuteAsync(new SharePrivateList(User.GetUserId(), name));

            return Ok();
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequestData addItemRequestData)
        {
            var item = await _domainService.ExecuteAsync(new AddPrivateItem(User.GetUserId(),
                addItemRequestData.BookId));

            return CreatedAtAction(nameof(GetItem), new {itemId = item.Id}, item);
        }

        [HttpGet("items/{itemId}")]
        public async Task<IActionResult> GetItem([FromRoute] int itemId)
        {
            var savedItem =
                await _domainService.AskAsync(new GetPrivateListItem(itemId, User.GetUserId()));

            return Ok(savedItem);
        }

        [HttpPatch("items/{itemId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int itemId, [FromBody] UpdatePrivateItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new UpdatePrivateListItem(User.GetUserId(), itemId, requestData.Status));

            return Ok(item);
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int itemId)
        {
            await _domainService.ExecuteAsync(new DeletePrivateItem(itemId, User.GetUserId()));

            return Ok();
        }
    }
}