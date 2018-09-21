using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class PrivateBookListRm : BookListRm
    {
        public IEnumerable<PrivateBookListItemRm> Items { get; set; }
    }
}