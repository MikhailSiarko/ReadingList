using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Queries.SharedList
{
    public class GetSharedListItemQuery : GetPrivateListItemQuery
    {
        public GetSharedListItemQuery(string login, BookInfo bookInfo) : base(login, bookInfo)
        {
        }
    }
}