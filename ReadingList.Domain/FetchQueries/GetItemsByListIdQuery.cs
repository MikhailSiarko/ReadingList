namespace ReadingList.Domain.FetchQueries
{
    public class GetItemsByListIdQuery
    {
        public readonly int ListId;

        public GetItemsByListIdQuery(int listId)
        {
            ListId = listId;
        }
    }
}