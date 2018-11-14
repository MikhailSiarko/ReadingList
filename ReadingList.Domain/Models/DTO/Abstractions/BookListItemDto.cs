namespace ReadingList.Domain.Models.DTO.Abstractions
{
    public class BookListItemDto : Entity
    {
        public string Title { get; set; }

        public string Author { get; set; }
        
        public string GenreId { get; set; }
    }
}