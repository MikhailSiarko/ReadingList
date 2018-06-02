using System.Collections.Generic;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.DTO.BookList
{
    public class PrivateBookListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }

        public IEnumerable<PrivateBookListItemDto> Items { get; set; }
    }
}