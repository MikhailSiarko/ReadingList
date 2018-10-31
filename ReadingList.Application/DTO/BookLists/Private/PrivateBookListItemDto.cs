using ReadingList.Application.DTO.BookList.Abstractions;

namespace ReadingList.Application.DTO.BookList
{
    public class PrivateBookListItemDto : BookListItemDto
    {
        public int ListId { get; set; }
        
        public int Status { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}