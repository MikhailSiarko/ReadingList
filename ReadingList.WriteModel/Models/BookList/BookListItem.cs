using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class BookListItem : Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public BookList BookList { get; set; }
        public int BookListId { get; set; }
    }
}