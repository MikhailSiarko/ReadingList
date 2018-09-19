using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListItemRm : BookListItemRm
    {
        public SharedBookListItemRm()
        {
            Tags = new List<string>();
        }
        
        public string Genre { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}