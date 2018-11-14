using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Extensions;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Api.RequestData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [ListRoute("Private")]
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
            var bookList = await _domainService.AskAsync(new GetPrivateListQuery(User.Claims.GetUserId()));

            return Ok(bookList);
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePrivateListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(new UpdatePrivateListCommand(User.Claims.GetUserId(),
                requestData.Name));

            return Ok(list);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] BookInfo bookInfo)
        {
            var item = await _domainService.ExecuteAsync(new AddPrivateItemCommand(User.Claims.GetUserId(), bookInfo));
            
            return Ok(item);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var savedItem = await _domainService.AskAsync(new GetPrivateListItemQuery(id, User.Claims.GetUserId()));

            return Ok(savedItem);
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdatePrivateItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new UpdatePrivateListItemCommand(User.Claims.GetUserId(), id,
                requestData.Status));
            
            return Ok(item);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await _domainService.ExecuteAsync(new DeletePrivateItemCommand(id, User.Claims.GetUserId()));

            return Ok();
        }
    }
}