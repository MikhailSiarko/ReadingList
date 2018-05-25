using Newtonsoft.Json;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.DTO.BookList
{
    public class PrivateBookListDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]
        public int OwnerId { get; set; }
        [JsonIgnore]
        public BookListType Type { get; set; }
    }
}