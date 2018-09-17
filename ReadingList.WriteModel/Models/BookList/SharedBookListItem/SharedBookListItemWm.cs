using System.Collections.Generic;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class SharedBookListItemWm : BookListItemWm
    {
        public List<SharedBookListItemTagWm> SharedBookListItemTags { get; set; }

        public CategoryWm Category { get; set; }

        public int? CategoryId { get; set; }

        public SharedBookListItemWm()
        {
            SharedBookListItemTags = new List<SharedBookListItemTagWm>();
        }
    }
}