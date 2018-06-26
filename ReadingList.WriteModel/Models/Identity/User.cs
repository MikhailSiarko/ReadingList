using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class User : Entity
    {
        public User()
        {
            BookLists = new List<BookList>();
        }
        public string Login { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public Profile Profile { get; set; }

        public int ProfileId { get; set; }

        public List<BookList> BookLists { get; set; }
    }
}