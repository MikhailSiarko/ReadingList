using System.Collections.Generic;

namespace ReadingList.Domain.Queries
{
    public class GetListsByIds
    {
        public readonly IEnumerable<int> ListsIds;

        public GetListsByIds(IEnumerable<int> listsIds)
        {
            ListsIds = listsIds;
        }
    }
}