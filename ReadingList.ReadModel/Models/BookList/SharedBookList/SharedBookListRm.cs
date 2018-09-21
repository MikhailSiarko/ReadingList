using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListRm : BookListRm
    {   
        public IEnumerable<string> Tags { get; set; }
    }
}