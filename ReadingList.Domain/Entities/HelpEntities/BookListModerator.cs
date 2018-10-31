using ReadingList.Domain.Entities.Identity;

namespace ReadingList.Domain.Entities.HelpEntities
{
    public class BookListModerator
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int BookListId { get; set; }

        public BookList BookList { get; set; }
    }
}