using ReadingList.Models.Infrastructure;

namespace ReadingList.Models.Write
{
    public class BookListItem : Entity
    {
        [IgnoreUpdate]
        public int BookId { get; set; }

        [IgnoreUpdate]
        public Book Book { get; set; }

        [IgnoreUpdate]
        public BookList BookList { get; set; }

        [IgnoreUpdate]
        public int BookListId { get; set; }
    }
}