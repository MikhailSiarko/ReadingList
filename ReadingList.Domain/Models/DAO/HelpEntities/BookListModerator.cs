using ReadingList.Domain.Models.DAO.Identity;

namespace ReadingList.Domain.Models.DAO.HelpEntities
{
    public class BookListModerator
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int BookListId { get; set; }

        public BookList BookList { get; set; }
    }
}