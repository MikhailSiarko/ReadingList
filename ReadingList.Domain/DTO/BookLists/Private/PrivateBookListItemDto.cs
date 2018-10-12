using ReadingList.Domain.DTO.BookList.Abstractions;

namespace ReadingList.Domain.DTO.BookList
{
    public class PrivateBookListItemDto : BookListItemDto
    {
        public int Status { get; set; }

        public double ReadingTimeInSeconds { get; set; }
    }
}