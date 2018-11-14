using System.Collections.Generic;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedListItemCommand : UpdateListItemCommand<SharedBookListItemDto>
    {
        public readonly int ListId;

        public readonly IEnumerable<string> Tags;
        
        public UpdateSharedListItemCommand(int userId, int itemId, int listId, IEnumerable<string> tags) 
            : base(userId, itemId)
        {
            ListId = listId;
            Tags = tags;
        }
    }
}