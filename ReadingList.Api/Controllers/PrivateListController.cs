using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Domain.Queries.PrivateList;

namespace ReadingList.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PrivateListController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var queryResult = await AskAsync(new GetPrivateListQuery(User.Identity.Name));
            return Json(queryResult.Data);
        }
    }
}