using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListRm : BookListRm
    {
        public SharedBookListRm()
        {
            Tags = new List<string>();
        }
        
        public IEnumerable<string> Tags { get; set; }
    }
}