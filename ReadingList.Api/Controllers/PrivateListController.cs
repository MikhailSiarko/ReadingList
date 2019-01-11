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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePrivateListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(new UpdatePrivateList(User.GetUserId(),
                requestData.Name));

            return Ok(list);
        }

        [HttpPost("share")]
        public async Task<IActionResult> Share([FromQuery] string name)
        {
            await _domainService.ExecuteAsync(new SharePrivateList(User.GetUserId(), name));

            return Ok();
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequestData addItemRequestData)
        {
            var item = await _domainService.ExecuteAsync(new AddPrivateItem(User.GetUserId(),
                addItemRequestData.BookId));

            return Ok(item);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var savedItem = await _domainService.AskAsync(new GetPrivateListItem(id, User.GetUserId()));

            return Ok(savedItem);
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id,
            [FromBody] UpdatePrivateItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new UpdatePrivateListItem(User.GetUserId(), id,
                requestData.Status));

            return Ok(item);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await _domainService.ExecuteAsync(new DeletePrivateItem(id, User.GetUserId()));

            return Ok();
        }
    }
}