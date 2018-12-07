using System.Collections.Generic;
using ReadingList.Models.Write.HelpEntities;

namespace ReadingList.Models.Write
{
    public class Book : Entity
    {
        public Book()
        {
            BookTags = new List<BookTag>();
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public Genre Genre { get; set; }

        public string GenreId { get; set; }

        public IEnumerable<BookTag> BookTags { get; set; }
    }
}