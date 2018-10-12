using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListRm : SimplifiedSharedBookListRm
    {
        public IEnumerable<SharedBookListItemRm> Items { get; set; }
    }
}