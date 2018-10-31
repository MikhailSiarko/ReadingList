using System.Collections.Generic;
using ReadingList.Domain.Entities.Base;
using ReadingList.Domain.Entities.HelpEntities;

namespace ReadingList.Domain.Entities.Identity
{
    public class User : Entity
    {
        public string Login { get; set; }

        public string Password { get; set; }
        
        public Role Role { get; set; }

        public int RoleId { get; set; }

        public Profile Profile { get; set; }

        public int ProfileId { get; set; }

        public IEnumerable<BookList> BookLists { get; set; }

        public IEnumerable<BookListModerator> BookListModerators { get; set; }
    }
}