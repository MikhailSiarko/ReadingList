using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class TagWm : NamedEntityWm
    {
        public List<BookTagWm> BookTags { get; set; }

        public List<SharedBookListItemTagWm> SharedBookListItemTags { get; set; }

        public List<SharedBookListTagWm> SharedBookListTags { get; set; }

        public TagWm()
        {
            BookTags = new List<BookTagWm>();
            SharedBookListItemTags = new List<SharedBookListItemTagWm>();
            SharedBookListTags = new List<SharedBookListTagWm>();
        }
    }
}