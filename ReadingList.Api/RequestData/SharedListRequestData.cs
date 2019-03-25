using System.Collections.Generic;
using ReadingList.Models.Write;

namespace ReadingList.Api.RequestData
{
    public struct SharedListRequestData
    {
        public string Name { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<int> Moderators { get; set; }
    }
}