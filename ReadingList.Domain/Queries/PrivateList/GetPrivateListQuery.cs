using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries
{
    public class GetPrivateListQuery : IQuery<PrivateBookListDto>
    {
        public readonly string Login;

        public GetPrivateListQuery(string login)
        {
            Login = login;
        }
    }
}