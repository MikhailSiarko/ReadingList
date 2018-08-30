using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class PrivateBookListRm : BookListRm
    {
        public PrivateBookListRm()
        {
            Items = new List<PrivateBookListItemRm>();
        }

        public List<PrivateBookListItemRm> Items { get; set; }
    }
}