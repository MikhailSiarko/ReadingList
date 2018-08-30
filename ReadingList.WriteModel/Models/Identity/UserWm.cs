using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class UserWm : EntityWm
    {
        public UserWm()
        {
            BookLists = new List<BookListWm>();
        }
        public string Login { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public ProfileWm Profile { get; set; }

        public int ProfileId { get; set; }

        public List<BookListWm> BookLists { get; set; }
    }
}