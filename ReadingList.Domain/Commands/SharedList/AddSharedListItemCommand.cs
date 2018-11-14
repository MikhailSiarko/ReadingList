using System.Collections.Generic;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class AddSharedListItemCommand : AddListItemCommand<SharedBookListItemDto>
    {
        public readonly int ListId;

        public readonly IEnumerable<string> Tags;

        public AddSharedListItemCommand(int listId, int userId, BookInfo bookInfo, IEnumerable<string> tags) : base(userId, bookInfo)
        {
            Tags = tags;
            ListId = listId;
        }
    }
}