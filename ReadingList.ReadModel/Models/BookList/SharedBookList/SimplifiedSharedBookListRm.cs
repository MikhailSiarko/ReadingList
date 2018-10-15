using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SimplifiedSharedBookListRm : BookListRm
    {
        public int BooksCount { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
    }
}