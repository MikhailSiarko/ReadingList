using System.Collections.Generic;

namespace ReadingList.Domain.DTO.BookList
{
    public class PrivateBookListDto
    {
        public PrivateBookListDto()
        {
            Items = new List<PrivateBookListItemDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }

        public IEnumerable<PrivateBookListItemDto> Items { get; }
    }
}