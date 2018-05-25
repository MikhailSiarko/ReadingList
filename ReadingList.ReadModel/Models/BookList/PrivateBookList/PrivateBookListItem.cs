namespace ReadingList.ReadModel.BookList.Models
{
    public class PrivateBookListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Status { get; set; }
        public long ReadingTimeInTicks { get; set; }
    }
}