using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class BookList : NamedEntity
    {
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public BookListType Type { get; set; }
        public string JsonFields { get; set; }
    }
}