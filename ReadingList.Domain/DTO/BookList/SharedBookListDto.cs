using System.Collections.Generic;
using Newtonsoft.Json;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.DTO.BookList
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SharedBookListDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]
        public BookListType Type { get; set; }
        [JsonIgnore]
        public int OwnerId { get; set; }
        [JsonProperty("Category")]
        public string Category { get; set; }
        [JsonProperty("Tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}