using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries.SharedList
{
    public class GetSharedListsQuery : SecuredQuery<IEnumerable<SharedBookListDto>>
    {
        public GetSharedListsQuery(string userLogin) : base(userLogin)
        {
        }
    }
}