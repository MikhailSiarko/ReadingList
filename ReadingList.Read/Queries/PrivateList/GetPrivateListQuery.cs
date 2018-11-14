using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Read.Queries
{
    public class GetPrivateListQuery : SecuredQuery<PrivateBookListDto>
    {
        public GetPrivateListQuery(int userId) : base(userId)
        {
        }
    }
}