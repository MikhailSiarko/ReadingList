using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

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

        public IEnumerable<BookListWm> BookLists { get; set; }

        public IEnumerable<BookListModeratorWm> BookListModerators { get; set; }
    }
}