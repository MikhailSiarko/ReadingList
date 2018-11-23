using System.Collections.Generic;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Models.DAO.HelpEntities;
using ReadingList.Domain.Models.DAO.Identity;

namespace ReadingList.Domain.Models.DAO
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