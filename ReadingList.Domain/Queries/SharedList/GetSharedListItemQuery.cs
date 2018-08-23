namespace ReadingList.Domain.Queries.SharedList
{
    public class GetSharedListItemQuery : GetPrivateListItemQuery
    {
        public GetSharedListItemQuery(string login, string title, string author) : base(login, title, author)
        {
        }
    }
}