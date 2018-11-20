using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries.Book;

namespace ReadingList.Api.Controllers
{
    [Authorize]
    [ApiRoute("[controller]")]
    public class BooksController : Controller
    {
        private readonly IDomainService _domainService;

        public BooksController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query)
        {
            var books = await _domainService.AskAsync(new FindBooksQuery(query));

            return Ok(books);
        }
    }
}