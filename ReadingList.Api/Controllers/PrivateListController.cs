using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Abstractions;
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
            return Json(queryResult.Data);
        }
    }
}