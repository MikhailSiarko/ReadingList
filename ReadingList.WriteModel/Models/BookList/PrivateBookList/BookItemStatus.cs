using System.Collections;
using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class BookItemStatus : NamedEntity
    {
    }
    
    public class BookItemStatuses : IEnumerable<BookItemStatus>
    {
        private readonly IEnumerable<BookItemStatus> _bookItemStatuses;

        public BookItemStatuses()
        {
            _bookItemStatuses = new List<BookItemStatus>
            {
                new BookItemStatus {Id = "5934c9e0-5ccd-4b7d-987b-cfa3762fe2c1", Name = "ToReading"},
                new BookItemStatus {Id = "34411095-3ce4-46a2-86cd-23f32f3f3279", Name = "Reading"},
                new BookItemStatus {Id = "c6a40658-a186-4a44-a78b-561211d0aec5", Name = "StartedButPostponed"},
                new BookItemStatus {Id = "ccc3fcce-a171-4189-90d3-37896e27e554", Name = "StartedButВiscarded"},
                new BookItemStatus {Id = "75e668b0-abe9-405c-9df7-5e9c97970114", Name = "Read"}
            };
        }

        public IEnumerator<BookItemStatus> GetEnumerator()
        {
            return _bookItemStatuses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}