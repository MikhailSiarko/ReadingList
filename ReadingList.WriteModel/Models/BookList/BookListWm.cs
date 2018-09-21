using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class BookListWm : NamedEntityWm
    {
        [IgnoreUpdate]
        public UserWm Owner { get; set; }

        [IgnoreUpdate]
        public int OwnerId { get; set; }

        [IgnoreUpdate]
        public BookListType Type { get; set; }
        
        public IEnumerable<SharedBookListTagWm> SharedBookListTags { get; set; }

        public IEnumerable<BookListModeratorWm> BookListModerators { get; set; }
    }
}