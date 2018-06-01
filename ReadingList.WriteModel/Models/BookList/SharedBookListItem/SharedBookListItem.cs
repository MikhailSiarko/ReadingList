using System.Collections.Generic;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class SharedBookListItem : BookListItem
    {
        public List<SharedBookListItemTag> SharedBookListItemTags { get; set; }

        public SharedBookListItem()
        {
            SharedBookListItemTags = new List<SharedBookListItemTag>();
        }
    }
}