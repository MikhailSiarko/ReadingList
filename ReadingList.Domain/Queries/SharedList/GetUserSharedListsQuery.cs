using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries
{
    public class GetUserSharedListsQuery : SecuredQuery<IEnumerable<SharedBookListDto>>
    {
        public GetUserSharedListsQuery(string userLogin) : base(userLogin)
        {
        }
    }
}