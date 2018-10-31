using System.Collections.Generic;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure;

namespace ReadingList.Application.Commands
{
    public class UpdateSharedListItemCommand : UpdateListItemCommand<SharedBookListItemDto>
    {
        public readonly int ListId;

        public readonly IEnumerable<string> Tags;
        
        public UpdateSharedListItemCommand(string userLogin, int itemId, int listId, BookInfo bookInfo,
            IEnumerable<string> tags) : base(userLogin, itemId, bookInfo)
        {
            ListId = listId;
            Tags = tags;
        }
    }
}