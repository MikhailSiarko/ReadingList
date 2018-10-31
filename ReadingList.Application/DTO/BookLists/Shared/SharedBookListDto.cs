using System.Collections.Generic;

namespace ReadingList.Application.DTO.BookList
{
    public class SharedBookListDto : SharedBookListPreviewDto
    {
        public IEnumerable<SharedBookListItemDto> Items { get; set; }
    }
}