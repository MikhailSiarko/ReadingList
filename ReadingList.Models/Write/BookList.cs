using System.Collections.Generic;
using ReadingList.Models.Infrastructure;
using ReadingList.Models.Write.HelpEntities;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Models.Write
{
    public class BookList : NamedEntity
    {
        public BookList()
        {
            SharedBookListTags = new List<SharedBookListTag>();
            BookListModerators = new List<BookListModerator>();
        }

        [IgnoreUpdate]
        public User Owner { get; set; }

        [IgnoreUpdate]
        public int OwnerId { get; set; }

        [IgnoreUpdate]
        public BookListType Type { get; set; }

        public IEnumerable<SharedBookListTag> SharedBookListTags { get; set; }

        public IEnumerable<BookListModerator> BookListModerators { get; set; }
    }
}