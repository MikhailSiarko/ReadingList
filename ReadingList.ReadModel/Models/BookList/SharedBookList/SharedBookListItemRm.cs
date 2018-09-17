using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListItemRm : BookListItemRm
    {
        public SharedBookListItemRm()
        {
            Tags = new List<string>();
        }
        
        public string Category { get; set; }

        public List<string> Tags { get; set; }
    }
}