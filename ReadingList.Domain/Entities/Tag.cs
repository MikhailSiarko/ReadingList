using System.Collections.Generic;
using ReadingList.Domain.Entities.Base;
using ReadingList.Domain.Entities.HelpEntities;

namespace ReadingList.Domain.Entities
{
    public class Tag : NamedEntity
    {
        public IEnumerable<BookTag> BookTags { get; set; }

        public IEnumerable<SharedBookListItemTag> SharedBookListItemTags { get; set; }

        public IEnumerable<SharedBookListTag> SharedBookListTags { get; set; }
    }
}