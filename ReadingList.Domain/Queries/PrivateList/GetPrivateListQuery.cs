using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries
{
    public class GetPrivateListQuery : SecuredQuery<PrivateBookListDto>
    {
        public GetPrivateListQuery(string login) : base(login)
        {
        }
    }
}