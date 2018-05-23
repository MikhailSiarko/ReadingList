using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class BookList : Entity
    {
        public User Owner { get; set; }
        public string OwnerId { get; set; }
        public List<BookListItem> Items { get; set; }
    }
}