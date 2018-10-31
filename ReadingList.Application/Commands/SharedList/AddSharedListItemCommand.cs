using System.Collections.Generic;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure;

namespace ReadingList.Application.Commands
{
    public class AddSharedListItemCommand : AddListItemCommand<SharedBookListItemDto>
    {
        public readonly int ListId;

        public readonly IEnumerable<string> Tags;

        public AddSharedListItemCommand(int listId, string login, BookInfo bookInfo, IEnumerable<string> tags) : base(login, bookInfo)
        {
            Tags = tags;
            ListId = listId;
        }
    }
}