using System.Collections.Generic;
using ReadingList.Domain.Models.DAO.HelpEntities;

namespace ReadingList.Domain.Models.DAO
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