namespace ReadingList.Domain.FetchQueries
{
    public class GetBookListItemQuery
    {
        public readonly string Author;

        public readonly string Title;

        public readonly int ListId;

        public GetBookListItemQuery(string author, string title, int listId)
        {
            Author = author;
            Title = title;
            ListId = listId;
        }
    }
}