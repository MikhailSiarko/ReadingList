using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList.Abstractions;

namespace ReadingList.Domain.DTO.BookList
{
    public class PrivateBookListDto : BookListDto
    {
        public IEnumerable<PrivateBookListItemDto> Items { get; set; }
    }
}