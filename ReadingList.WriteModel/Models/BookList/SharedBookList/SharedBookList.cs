using System.Collections.Generic;

namespace ReadingList.WriteModel.Models
{
    public class SharedBookList : BookList
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public List<Tag> Tags { get; set; }
        public new List<SharedBookListItem> Items { get; set; }
    }
}