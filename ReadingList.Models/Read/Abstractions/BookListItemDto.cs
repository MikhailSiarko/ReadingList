namespace ReadingList.Models.Read.Abstractions
{
    public class BookListItemDto : Entity
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }
    }
}