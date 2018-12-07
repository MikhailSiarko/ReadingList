using ReadingList.Models.Write.Identity;

namespace ReadingList.Models.Write.HelpEntities
{
    public class BookListModerator
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int BookListId { get; set; }

        public BookList BookList { get; set; }
    }
}