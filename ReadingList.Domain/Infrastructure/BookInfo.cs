namespace ReadingList.Domain.Infrastructure
{
    public class BookInfo
    {
        public readonly string Title;
        
        public readonly string Author;

        public readonly string GenreId;

        public BookInfo(string title, string author)
        {
            Title = title;
            Author = author;
        }
        
        public BookInfo(string title, string author, string genreId)
        {
            Title = title;
            Author = author;
            GenreId = genreId;
        }
    }
}