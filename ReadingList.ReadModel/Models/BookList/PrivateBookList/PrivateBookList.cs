using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class PrivateBookList
    {
        public PrivateBookList()
        {
            Items = new List<PrivateBookListItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public List<PrivateBookListItem> Items { get; set; }
    }
}