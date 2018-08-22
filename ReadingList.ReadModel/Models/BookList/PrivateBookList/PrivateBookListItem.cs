namespace ReadingList.ReadModel.Models
{
    public class PrivateBookListItem : BookListItem
    {
        public int Status { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}