namespace ReadingList.Domain.DTO.BookList.Abstractions
{
    public class BookListDto : EntityDto
    {
        public string Name { get; set; }

        public int OwnerId { get; set; }

        public int Type { get; set; }
    }
}