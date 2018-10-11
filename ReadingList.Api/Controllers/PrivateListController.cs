using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Api.Infrastructure.Filters;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services;
using ReadingList.WriteModel.Models;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [ListRoute(BookListType.Private)]
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
            var bookList = await _domainService.AskAsync(new GetPrivateListQuery(User.Identity.Name));

            return Ok(bookList);
        }
        
        [HttpPut]
        [ValidateModelState]
        public async Task<IActionResult> Put([FromBody] UpdatePrivateListData listData)
        {
            var list = await _domainService.ExecuteAsync(new UpdatePrivateListCommand(User.Identity.Name, listData.Name));

            return Ok(list);
        }

        [HttpPost("items")]
        [ValidateModelState]
        public async Task<IActionResult> AddItem([FromBody] AddItemToPrivateListData addItemData)
        {
            var item = await _domainService.ExecuteAsync(new AddPrivateItemCommand(User.Identity.Name,
                new BookInfo(addItemData.Title, addItemData.Author, addItemData.GenreId)));
            
            return Ok(item);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var savedItem = await _domainService.AskAsync(new GetPrivateListItemQuery(id, User.Identity.Name));

            return Ok(savedItem);
        }

        [HttpPut("items/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdatePrivateListItemData updateItemData)
        {
            var item = await _domainService.ExecuteAsync(new UpdatePrivateListItemCommand(User.Identity.Name, id,
                new BookInfo(updateItemData.Title, updateItemData.Author, updateItemData.GenreId), updateItemData.Status));
            
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