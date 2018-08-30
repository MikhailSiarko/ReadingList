namespace ReadingList.ReadModel.Models
{
    public class PrivateBookListItemRm : BookListItemRm
    {
        public int Status { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}