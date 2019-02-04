using System.Collections.Generic;
using ReadingList.Models.Write.HelpEntities;

namespace ReadingList.Models.Write
{
    public class Tag : NamedEntity
    {
        public Tag()
        {
            BookTags = new List<BookTag>();
            SharedBookListTags = new List<SharedBookListTag>();
        }

        public IEnumerable<BookTag> BookTags { get; set; }

        public IEnumerable<SharedBookListTag> SharedBookListTags { get; set; }
    }
}