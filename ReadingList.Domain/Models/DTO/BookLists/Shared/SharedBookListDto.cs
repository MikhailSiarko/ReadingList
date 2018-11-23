using System.Collections.Generic;

namespace ReadingList.Domain.Models.DTO.BookLists
{
    public class SharedBookListDto : SharedBookListPreviewDto
    {
        public IEnumerable<SharedBookListItemDto> Items { get; set; }

        public bool Editable { get; set; }
    }
}