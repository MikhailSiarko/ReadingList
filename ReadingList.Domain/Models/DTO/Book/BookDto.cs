using System.Collections.Generic;

namespace ReadingList.Domain.Models.DTO.Book
{
    public class BookDto : Entity
    {
        public BookDto()
        {
            Tags = new List<string>();
        }

        public string Author { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}