using System.Collections.Generic;
using ReadingList.Domain.Models.DAO.HelpEntities;

namespace ReadingList.Domain.Models.DAO
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