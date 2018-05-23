namespace ReadingList.ReadModel.BookList.Models
{
    public class PrivateBookListItem
    {
        public string Id { get; set; }
        public object Book { get; set; }
        public string Status { get; set; }
        public long ReadingTime { get; set; }
    }
}