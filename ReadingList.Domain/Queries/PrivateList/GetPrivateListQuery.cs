using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Queries
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