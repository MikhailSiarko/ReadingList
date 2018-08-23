using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReadingList.Domain.DTO.BookList
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SharedBookListDto
    {
        public SharedBookListDto()
        {
            Items = new List<PrivateBookListItemDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }
        
        public IEnumerable<PrivateBookListItemDto> Items { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}