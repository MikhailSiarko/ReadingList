using ReadingList.ReadModel.BookList.Models;

namespace ReadingList.Domain.Queries.PrivateList
{
    public class GetPrivateListQuery : IQuery<PrivateBookList>
    {
        public readonly string Username;

        public GetPrivateListQuery(string username)
        {
            Username = username;
        }
    }
}