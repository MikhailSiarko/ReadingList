using ReadingList.ReadModel.BookList.Models;

namespace ReadingList.Domain.Queries.PrivateList
{
    public class GetPrivateListQuery : IQuery<PrivateBookList>
    {
        public readonly string Login;

        public GetPrivateListQuery(string login)
        {
            Login = login;
        }
    }
}