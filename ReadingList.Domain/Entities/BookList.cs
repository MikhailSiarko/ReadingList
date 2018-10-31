using System.Collections.Generic;
using ReadingList.Domain.Entities.Base;
using ReadingList.Domain.Entities.HelpEntities;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Domain.Enumerations;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Entities
{
    public class BookList : NamedEntity
    {
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