namespace ReadingList.Application.DTO.BookList.Abstractions
{
    public class BookListItemDto : EntityDto
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }
}