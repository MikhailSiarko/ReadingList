using System.Collections.Generic;
using ReadingList.Models.Read.Abstractions;

namespace ReadingList.Models.Read
{
    public class SharedBookListItemDto : BookListItemDto
    {
        public int ListId { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}