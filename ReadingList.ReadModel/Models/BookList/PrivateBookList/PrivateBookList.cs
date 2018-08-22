using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class PrivateBookList : BookList
    {
        public PrivateBookList()
        {
            Items = new List<PrivateBookListItem>();
        }

        public List<PrivateBookListItem> Items { get; set; }
    }
}