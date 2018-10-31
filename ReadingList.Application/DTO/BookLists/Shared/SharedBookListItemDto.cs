using System.Collections.Generic;
using ReadingList.Application.DTO.BookList.Abstractions;

namespace ReadingList.Application.DTO.BookList
{
    public class SharedBookListItemDto : BookListItemDto
    {
        public int ListId { get; set; }

        public string GenreId { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
