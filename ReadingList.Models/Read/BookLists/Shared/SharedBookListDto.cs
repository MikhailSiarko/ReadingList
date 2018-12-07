using System.Collections.Generic;

namespace ReadingList.Models.Read
{
    public class SharedBookListDto : SharedBookListPreviewDto
    {
        public IEnumerable<SharedBookListItemDto> Items { get; set; }

        public bool Editable { get; set; }
    }
}