using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetPrivateList : SecuredQuery<PrivateBookListDto>
    {
        public GetPrivateList(int userId) : base(userId)
        {
        }
    }
}