namespace ReadingList.Domain.Queries
{
    public class GetItemsByListId
    {
        public readonly int ListId;

        public GetItemsByListId(int listId)
        {
            ListId = listId;
        }
    }
}