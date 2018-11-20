namespace ReadingList.Domain.FetchQueries
{
    public class GetBookListItemQuery
    {
        public readonly int BookId;

        public readonly int ListId;

        public GetBookListItemQuery(int bookId, int listId)
        {
            BookId = bookId;
            ListId = listId;
        }
    }
}