using System.Collections.Generic;
using ReadingList.Domain.Models.DTO.Abstractions;

namespace ReadingList.Domain.Models.DTO.BookLists
{
    public class SharedBookListItemDto : BookListItemDto
    {
        public int ListId { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
