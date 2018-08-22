using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookList : BookList
    {
        public SharedBookList()
        {
            Items = new List<SharedBookListItem>();
        }

        public string JsonFields { get; set; }

        public List<SharedBookListItem> Items { get; set; }
    }
}