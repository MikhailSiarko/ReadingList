using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class BookListItem : Entity
    {
        public Book Book { get; set; }
        public string BookListId { get; set; }
    }
}