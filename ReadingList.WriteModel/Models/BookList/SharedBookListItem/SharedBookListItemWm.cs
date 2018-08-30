using System.Collections.Generic;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class SharedBookListItemWm : BookListItemWm
    {
        public List<SharedBookListItemTagWm> SharedBookListItemTags { get; set; }

        public SharedBookListItemWm()
        {
            SharedBookListItemTags = new List<SharedBookListItemTagWm>();
        }
    }
}