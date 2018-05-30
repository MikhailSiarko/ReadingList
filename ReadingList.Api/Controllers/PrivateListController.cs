using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Abstractions;
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
            var queryResult = await AskAsync(new GetPrivateListQuery(User.Identity.Name));
            
            if(queryResult.IsSucceed)
                return Ok(queryResult);
            
            return NotFound(queryResult);
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePrivateListData listData)
        {
            var result = await ExecuteAsync(new UpdatePrivateListCommand(User.Identity.Name, listData.Name));
            
            if(!result.IsSucceed)
                return StatusCode(500, result);
            
            return Ok();
        }

        [HttpPost("items")]
        public async Task<IActionResult> Post([FromBody] AddItemToPrivateListData addItemToPrivateListData)
        {
            var result = await ExecuteAsync(new AddPrivateItemCommand(User.Identity.Name,
                addItemToPrivateListData.Title, addItemToPrivateListData.Author));
            
            if (!result.IsSucceed)
                return StatusCode(500, result);
            
            var savedItemResult = await AskAsync(new GetPrivateListItemQuery(User.Identity.Name,
                addItemToPrivateListData.Title, addItemToPrivateListData.Author));
            
            if (!savedItemResult.IsSucceed)
                return NotFound(result);
            
            return Ok(savedItemResult);
        }
        
        [HttpPut("items/{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdatePrivateListItemData updatePrivateListItemData)
        {
            var result = await ExecuteAsync(new UpdatePrivateListItemCommand(id,
                updatePrivateListItemData.Title, updatePrivateListItemData.Author, updatePrivateListItemData.Status));
            
            if (!result.IsSucceed)
                return StatusCode(500, result);
            
            var itemResult = await AskAsync(new GetPrivateListItemQuery(User.Identity.Name,
                updatePrivateListItemData.Title, updatePrivateListItemData.Author));
            
            if (!itemResult.IsSucceed)
                return NotFound(result);
            
            return Ok(itemResult);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await ExecuteAsync(new RemovePrivateItemCommand(id));
            
            if (!result.IsSucceed)
                return NotFound(result);
            
            return Ok();
        }
    }
}