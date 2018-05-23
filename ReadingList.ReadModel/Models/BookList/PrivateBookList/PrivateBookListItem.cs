namespace ReadingList.ReadModel.BookList.Models
{
    public class PrivateBookListItem
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public long ReadingTimeInTicks { get; set; }
    }
}