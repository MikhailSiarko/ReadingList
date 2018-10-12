using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SimplifiedSharedBookListRm : BookListRm
    {   
        public IEnumerable<string> Tags { get; set; }
    }
}