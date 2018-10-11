using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
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