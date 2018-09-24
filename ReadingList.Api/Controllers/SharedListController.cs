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
    [ListRoute(BookListType.Shared)]
    public class SharedListController : Controller
    {
        private readonly IDomainService _domainService;

        public SharedListController(IDomainService domainService)
        {
            _domainService = domainService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var bookList = await _domainService.AskAsync(new GetSharedListQuery(id));

            return Ok(bookList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _domainService.ExecuteAsync(new DeleteSharedListCommand(User.Identity.Name, id));
            
            return Ok();
        }

        [HttpGet("own")]
        public async Task<IActionResult> Get()
        {
            var bookLists = await _domainService.AskAsync(new GetUserSharedListsQuery(User.Identity.Name));
            
            return Ok(bookLists);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CreateSharedListData sharedListData)
        {
            await _domainService.ExecuteAsync(new CreateSharedListCommand(User.Identity.Name, sharedListData.Name,
                sharedListData.Tags));

            return Ok();
        }
        
        [HttpPut("{id}")]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateSharedListData updateData)
        {
            await _domainService.ExecuteAsync(new UpdateSharedListCommand(User.Identity.Name, id, updateData.Name,
                updateData.Tags));

            return Ok();
        }
        
        [HttpGet("{listId}/items")]
        public async Task<IActionResult> GetItems([FromRoute] int listId)
        {
            var items = await _domainService.AskAsync(new GetSharedListItemsQuery(listId));

            return Ok(items);
        }

        [HttpPost("{listId}/items")]
        [ValidateModelState]
        public async Task<IActionResult> AddItem([FromRoute] int listId, [FromBody] AddItemToSharedListData addItemData)
        {
            await _domainService.ExecuteAsync(new AddSharedListItemCommand(listId, User.Identity.Name, 
                new BookInfo(addItemData.Title, addItemData.Author, addItemData.GenreId), addItemData.Tags));

            return Ok();
        }
        
        [HttpGet("{listId}/items/{itemId}")]
        public async Task<IActionResult> GetItem([FromRoute] int listId, [FromRoute] int itemId)
        {
            var item = await _domainService.AskAsync(new GetSharedListItemQuery(listId, itemId));
            
            return Ok(item);
        }

        [HttpDelete("{listId}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int listId, [FromRoute] int itemId)
        {
            await _domainService.ExecuteAsync(new DeleteSharedListItemCommand(User.Identity.Name, listId, itemId));
            
            return Ok();
        }

        [HttpPut("{listId}/items/{itemId}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateItem([FromRoute] int listId, [FromRoute] int itemId, [FromBody] UpdateSharedListItemData updateItemData)
        {
            await _domainService.ExecuteAsync(new UpdateSharedListItemCommand(User.Identity.Name, itemId, listId,
                new BookInfo(updateItemData.Title, updateItemData.Author, updateItemData.GenreId),
                updateItemData.Tags));
            
            return Ok();
        }
    }
}