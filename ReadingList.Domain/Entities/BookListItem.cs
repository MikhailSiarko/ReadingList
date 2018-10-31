using ReadingList.Domain.Entities.Base;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Entities
{
    public class BookListItem : Entity
    {
        public string Title { get; set; }

        public string Author { get; set; }
        
        public string GenreId { get; set; }

        public Genre Genre { get; set; }

        [IgnoreUpdate]
        public BookList BookList { get; set; }

        [IgnoreUpdate]
        public int BookListId { get; set; }
    }
}