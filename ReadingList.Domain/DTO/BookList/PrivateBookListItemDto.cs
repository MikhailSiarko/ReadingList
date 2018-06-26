namespace ReadingList.Domain.DTO.BookList
{
    public class PrivateBookListItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Status { get; set; }

        public double ReadingTimeInSeconds { get; set; }
    }
}