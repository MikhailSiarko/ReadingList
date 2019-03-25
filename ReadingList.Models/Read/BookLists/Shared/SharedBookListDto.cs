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
            Moderators = new List<ModeratorDto>();
        }

        public IEnumerable<SharedBookListItemDto> Items { get; set; }

        public bool Editable { get; set; }

        public bool CanBeModerated { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }

        public IEnumerable<ModeratorDto> Moderators { get; set; }
    }
}