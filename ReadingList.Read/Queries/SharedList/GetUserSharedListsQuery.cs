using System.Collections.Generic;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Read.Queries
{
    public class GetUserSharedListsQuery : SecuredQuery<IEnumerable<SharedBookListPreviewDto>>
    {
        public GetUserSharedListsQuery(int userId) : base(userId)
        {
        }
    }
}