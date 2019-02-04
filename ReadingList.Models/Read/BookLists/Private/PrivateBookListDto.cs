using System.Collections.Generic;
using ReadingList.Models.Read.Abstractions;

namespace ReadingList.Models.Read
{
    public class PrivateBookListDto : BookListDto
    {
        public IEnumerable<PrivateBookListItemDto> Items { get; set; }
    }
}