using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListRm : BookListRm
    {
        public IEnumerable<SharedBookListItemRm> Items { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}