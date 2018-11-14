using System.Collections.Generic;
using ReadingList.Domain.Models.DTO.Abstractions;

namespace ReadingList.Domain.Models.DTO.BookLists
{
    public class PrivateBookListDto : BookListDto
    {
        public IEnumerable<PrivateBookListItemDto> Items { get; set; }
    }
}