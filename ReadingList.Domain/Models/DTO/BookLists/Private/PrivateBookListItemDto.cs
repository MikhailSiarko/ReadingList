using ReadingList.Domain.Models.DTO.Abstractions;

namespace ReadingList.Domain.Models.DTO.BookLists
{
    public class PrivateBookListItemDto : BookListItemDto
    {
        public int ListId { get; set; }

        public int Status { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}