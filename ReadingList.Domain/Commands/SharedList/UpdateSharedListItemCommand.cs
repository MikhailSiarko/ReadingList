using System.Collections.Generic;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands.SharedList
{
    public class UpdateSharedListItemCommand : UpdateListItemCommand
    {
        public readonly int ListId;

        public readonly IEnumerable<string> Tags;

        public readonly string GenreId;
        
        public UpdateSharedListItemCommand(string userLogin, int itemId, int listId, BookInfo bookInfo,
            IEnumerable<string> tags, string genreId) : base(userLogin, itemId, bookInfo)
        {
            ListId = listId;
            Tags = tags;
            GenreId = genreId;
        }
    }
}