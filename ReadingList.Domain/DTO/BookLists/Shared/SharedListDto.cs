using System.Collections.Generic;

namespace ReadingList.Domain.DTO.BookList
{
    public class SharedListDto : SimplifiedSharedBookListDto
    {
        public IEnumerable<SharedBookListItemDto> Items { get; set; }
    }
}