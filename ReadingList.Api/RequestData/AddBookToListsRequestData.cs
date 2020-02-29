using System.Collections.Generic;

namespace ReadingList.Api.RequestData
{
    public struct AddBookToListsRequestData
    {
        public int Id { get; set; }

        public IEnumerable<int> ListIds { get; set; }
    }
}