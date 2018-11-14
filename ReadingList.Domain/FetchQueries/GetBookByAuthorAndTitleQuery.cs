namespace ReadingList.Domain.FetchQueries
{
    public class GetBookByAuthorAndTitleQuery
    {
        public readonly string Author;

        public readonly string Title;

        public GetBookByAuthorAndTitleQuery(string author, string title)
        {
            Author = author;
            Title = title;
        }
    }
}