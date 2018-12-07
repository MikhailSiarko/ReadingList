namespace ReadingList.Domain.Queries
{
    public class GetBookListItem
    {
        public readonly int BookId;

        public readonly int ListId;

        public GetBookListItem(int bookId, int listId)
        {
            BookId = bookId;
            ListId = listId;
        }
    }
}