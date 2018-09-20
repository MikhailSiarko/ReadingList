namespace ReadingList.WriteModel.Models.HelpEntities
{
    public class BookListModeratorWm
    {
        public int UserId { get; set; }

        public UserWm User { get; set; }

        public int BookListId { get; set; }

        public BookListWm BookList { get; set; }
    }
}