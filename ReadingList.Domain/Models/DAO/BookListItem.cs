using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Models.DAO
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