using ReadingList.Models.Read.Abstractions;

namespace ReadingList.Models.Read
{
    public class PrivateBookListItemDto : BookListItemDto
    {
        public int ListId { get; set; }

        public int Status { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}