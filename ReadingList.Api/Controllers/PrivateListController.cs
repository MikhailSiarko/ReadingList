using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Abstractions;
using ReadingList.Api.Filters;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Queries;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PrivateListController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bookList = await AskAsync(new GetPrivateListQuery(User.Identity.Name));

            return Ok(bookList);
        }
        
        [HttpPut]
        [ValidateModelState]
        public async Task<IActionResult> Put([FromBody] UpdatePrivateListData listData)
        {
            await ExecuteAsync(new UpdatePrivateListCommand(User.Identity.Name, listData.Name));

            return Ok();
        }

        [HttpPost("items")]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] AddItemToPrivateListData addItemToPrivateListData)
        {
            await ExecuteAsync(new AddPrivateItemCommand(User.Identity.Name,
                addItemToPrivateListData.Title, addItemToPrivateListData.Author));

            var savedItem = await AskAsync(new GetPrivateListItemQuery(User.Identity.Name,
                addItemToPrivateListData.Title, addItemToPrivateListData.Author));
            
            return Ok(savedItem);
        }
        
        [HttpPut("items/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateListItem([FromRoute] int id, [FromBody] UpdatePrivateListItemData updatePrivateListItemData)
        {
            await ExecuteAsync(new UpdatePrivateListItemCommand(User.Identity.Name, id,
                updatePrivateListItemData.Title, updatePrivateListItemData.Author, updatePrivateListItemData.Status));

            var updatedItem = await AskAsync(new GetPrivateListItemQuery(User.Identity.Name,
                updatePrivateListItemData.Title, updatePrivateListItemData.Author));
            
            return Ok(updatedItem);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await ExecuteAsync(new RemovePrivateItemCommand(id, User.Identity.Name));

            return Ok();
        }
    }
}