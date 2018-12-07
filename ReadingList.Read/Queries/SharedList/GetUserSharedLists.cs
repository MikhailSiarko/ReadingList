using System.Collections.Generic;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetUserSharedLists : SecuredQuery<IEnumerable<SharedBookListPreviewDto>>
    {
        public GetUserSharedLists(int userId) : base(userId)
        {
        }
    }
}