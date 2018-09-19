using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReadingList.Domain.DTO.BookList
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SharedBookListDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }

        public int Type { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}