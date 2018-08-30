using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListRm : BookListRm
    {
        public SharedBookListRm()
        {
            Items = new List<SharedBookListItemRm>();
        }

        public string JsonFields { get; set; }

        public List<SharedBookListItemRm> Items { get; set; }
    }
}