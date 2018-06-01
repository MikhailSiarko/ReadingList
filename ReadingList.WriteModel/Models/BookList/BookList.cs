using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class BookList : NamedEntity
    {
        public User Owner { get; set; }
        [IgnoreUpdate]
        public int OwnerId { get; set; }
        [IgnoreUpdate]
        public BookListType Type { get; set; }
        public string JsonFields { get; set; }
    }
}