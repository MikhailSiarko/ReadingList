using System.Collections.Generic;
using ReadingList.Application.DTO.BookList.Abstractions;

namespace ReadingList.Application.DTO.BookList
{
    public class PrivateBookListDto : BookListDto
    {
        public IEnumerable<PrivateBookListItemDto> Items { get; set; }
    }
}