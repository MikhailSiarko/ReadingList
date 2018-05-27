using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Abstractions;
using ReadingList.Api.QueriesData;
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
            return StatusCode(500, queryResult);
        }

        [HttpPost("{title}/{author}")]
        public async Task<IActionResult> Post([FromQuery] AddItemToPrivateListData addItemToPrivateListData)
        {
            return Ok();
        }
        
        [HttpPut("{itemId}/{title}/{author}/status")]
        public async Task<IActionResult> Put([FromQuery] UpdatePrivateListItemData updatePrivateListItemData)
        {
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(int itemId)
        {
            return Ok();
        }
    }
}