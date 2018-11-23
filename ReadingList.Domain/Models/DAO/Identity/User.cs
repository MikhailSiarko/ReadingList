using System.Collections.Generic;

namespace ReadingList.Domain.Models.DAO.Identity
{
    public class User : Entity
    {
        public User()
        {
            BookLists = new List<BookList>();
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }

        public Profile Profile { get; set; }

        public int ProfileId { get; set; }

        public IEnumerable<BookList> BookLists { get; set; }
    }
}