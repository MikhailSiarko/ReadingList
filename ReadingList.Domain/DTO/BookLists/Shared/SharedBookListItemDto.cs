using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList.Abstractions;

namespace ReadingList.Domain.DTO.BookList
{
    public class SharedBookListItemDto : BookListItemDto
    {
        public int ListId { get; set; }

        public string GenreId { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
