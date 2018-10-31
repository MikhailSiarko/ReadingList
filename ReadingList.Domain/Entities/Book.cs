using System.Collections.Generic;
using ReadingList.Domain.Entities.Base;
using ReadingList.Domain.Entities.HelpEntities;

namespace ReadingList.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public Genre Genre { get; set; }

        public string  GenreId { get; set; }

        public IEnumerable<BookTag> BookTags { get; set; }
    }
}