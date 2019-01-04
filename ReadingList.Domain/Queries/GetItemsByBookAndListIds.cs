using System.Collections.Generic;

namespace ReadingList.Domain.Queries
{
    public class GetItemsByBookAndListIds
    {
        public readonly IEnumerable<int> ListsIds;

        public readonly int BookId;

        public GetItemsByBookAndListIds(IEnumerable<int> listsIds, int bookId)
        {
            ListsIds = listsIds;
            BookId = bookId;
        }
    }
}