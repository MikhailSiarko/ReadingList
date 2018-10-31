using System.Collections.Generic;
using ReadingList.Domain.Entities.HelpEntities;

namespace ReadingList.Domain.Entities
{
    public class SharedBookListItem : BookListItem
    {
        public IEnumerable<SharedBookListItemTag> SharedBookListItemTags { get; set; }
    }
}