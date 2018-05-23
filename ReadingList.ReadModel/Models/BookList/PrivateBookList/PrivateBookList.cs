using System.Collections.Generic;

namespace ReadingList.ReadModel.BookList.Models
{
    public class PrivateBookList
    {
        public PrivateBookList()
        {
            Items = new List<PrivateBookListItem>();
        }
        public string OwnerId { get; set; }
        public List<PrivateBookListItem> Items { get; set; }
    }
}