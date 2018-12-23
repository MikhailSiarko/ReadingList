using System.Collections.Generic;
using ReadingList.Models.Read.Abstractions;

namespace ReadingList.Models.Read
{
    public class SharedBookListDto : BookListDto
    {
        public SharedBookListDto()
        {
            Items = new List<SharedBookListItemDto>();
            Tags = new List<TagDto>();
        }

        public IEnumerable<SharedBookListItemDto> Items { get; set; }

        public bool Editable { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }
    }
}