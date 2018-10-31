using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Api.RequestData;
using ReadingList.Application.Commands;
using ReadingList.Application.Infrastructure;
using ReadingList.Application.Queries;
using ReadingList.Application.Services;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [ListRoute("Private")]
    public class PrivateListController : Controller
    {
        private readonly IApplicationService _domainService;

        public PrivateListController(IApplicationService domainService)
        {
            _domainService = domainService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bookList = await _domainService.AskAsync(new GetPrivateListQuery(User.Identity.Name));

            return Ok(bookList);
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePrivateListRequestData requestData)
        {
            var list = await _domainService.ExecuteAsync(new UpdatePrivateListCommand(User.Identity.Name,
                requestData.Name));

            return Ok(list);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] BookInfo bookInfo)
        {
            var item = await _domainService.ExecuteAsync(new AddPrivateItemCommand(User.Identity.Name, bookInfo));
            
            return Ok(item);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var savedItem = await _domainService.AskAsync(new GetPrivateListItemQuery(id, User.Identity.Name));

            return Ok(savedItem);
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdatePrivateItemRequestData requestData)
        {
            var item = await _domainService.ExecuteAsync(new UpdatePrivateListItemCommand(User.Identity.Name, id,
                new BookInfo
                {
                    Title = requestData.Title,
                    Author = requestData.Author
                }, requestData.Status));
            
            return Ok(item);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await _domainService.ExecuteAsync(new DeletePrivateItemCommand(id, User.Identity.Name));

            return Ok();
        }
    }
}