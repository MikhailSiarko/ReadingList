using System.Collections.Generic;
using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries
{
    public class GetUserSharedListsQuery : SecuredQuery<IEnumerable<SharedBookListPreviewDto>>
    {
        public GetUserSharedListsQuery(string userLogin) : base(userLogin)
        {
        }
    }
}