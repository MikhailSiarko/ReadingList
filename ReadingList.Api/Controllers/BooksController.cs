using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Extensions;
using ReadingList.Api.Infrastructure.Attributes;
using ReadingList.Domain.Commands;
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
            var books = await _domainService.AskAsync(new FindBooks(query));

            return Ok(books);
        }

        [HttpPost("{bookId}")]
        public async Task<IActionResult> Post([FromRoute] int bookId, [FromBody] IEnumerable<int> listsIds)
        {
            await _domainService.ExecuteAsync(new AddBookToLists(User.Claims.GetUserId(), bookId, listsIds));

            return Ok();
        }
    }
}