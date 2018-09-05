using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Filters;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [Route("api/list/private")]
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
            await _domainService.ExecuteAsync(new UpdatePrivateListCommand(User.Identity.Name, listData.Name));

            return Ok();
        }

        [HttpPost("items")]
        [ValidateModelState]
        public async Task<IActionResult> AddItem([FromBody] AddItemToPrivateListData addItemToPrivateListData)
        {
            await _domainService.ExecuteAsync(new AddPrivateItemCommand(User.Identity.Name,
                addItemToPrivateListData.Title, addItemToPrivateListData.Author));

            var savedItem = await _domainService.AskAsync(new GetPrivateListItemQuery(User.Identity.Name,
                addItemToPrivateListData.Title, addItemToPrivateListData.Author));
            
            return Ok(savedItem);
        }
        
        [HttpPut("items/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdatePrivateListItemData updatePrivateListItemData)
        {
            await _domainService.ExecuteAsync(new UpdatePrivateListItemCommand(User.Identity.Name, id,
                updatePrivateListItemData.Title, updatePrivateListItemData.Author, updatePrivateListItemData.Status));

            var updatedItem = await _domainService.AskAsync(new GetPrivateListItemQuery(User.Identity.Name,
                updatePrivateListItemData.Title, updatePrivateListItemData.Author));
            
            return Ok(updatedItem);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await _domainService.ExecuteAsync(new RemovePrivateItemCommand(id, User.Identity.Name));

            return Ok();
        }
    }
}