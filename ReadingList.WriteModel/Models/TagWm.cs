using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class TagWm : NamedEntityWm
    {
        public IEnumerable<BookTagWm> BookTags { get; set; }

        public IEnumerable<SharedBookListItemTagWm> SharedBookListItemTags { get; set; }

        public IEnumerable<SharedBookListTagWm> SharedBookListTags { get; set; }
    }
}