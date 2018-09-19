using System.Collections.Generic;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands.SharedList
{
    public class AddSharedListItemCommand : AddListItemCommand
    {
        public readonly int ListId;

        public readonly string GenreId;

        public readonly IEnumerable<string> Tags;

        public AddSharedListItemCommand(int listId, string login, BookInfo bookInfo, string genreId, IEnumerable<string> tags) : base(login, bookInfo)
        {
            ListId = listId;
            GenreId = genreId;
            Tags = tags;
        }
    }
}