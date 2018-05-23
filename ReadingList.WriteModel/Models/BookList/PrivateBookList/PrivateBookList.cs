using System.Collections.Generic;

namespace ReadingList.WriteModel.Models
{
    public class PrivateBookList : BookList
    {
        public new List<PrivateBookListItem> Items { get; set; }

        public PrivateBookList()
        {
            Items = new List<PrivateBookListItem>();
        }
    }
}