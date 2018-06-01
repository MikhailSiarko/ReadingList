using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class Tag : NamedEntity
    {
        public List<BookTag> BookTags { get; set; }
        public List<SharedBookListItemTag> SharedBookListItemTags { get; set; }

        public Tag()
        {
            BookTags = new List<BookTag>();
            SharedBookListItemTags = new List<SharedBookListItemTag>();
        }
    }
}