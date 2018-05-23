using System.Collections.Generic;

namespace ReadingList.ReadModel.BookList.Models
{
    public class PrivateBookList
    {
        public PrivateBookList()
        {
            Items = new List<PrivateBookListItem>();
        }
        public string UserId { get; set; }
        public IEnumerable<PrivateBookListItem> Items { get; set; }
    }
}