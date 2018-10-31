using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries
{
    public class GetPrivateListQuery : SecuredQuery<PrivateBookListDto>
    {
        public GetPrivateListQuery(string login) : base(login)
        {
        }
    }
}