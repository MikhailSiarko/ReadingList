namespace ReadingList.Domain.Infrastructure
{
    public class BookInfo
    {
        public readonly string Title;
        
        public readonly string Author;

        public BookInfo(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }
}