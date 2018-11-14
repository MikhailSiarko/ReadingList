namespace ReadingList.Domain.Models.DTO.Abstractions
{
    public class BookListDto : Entity
    {
        public string Name { get; set; }

        public int OwnerId { get; set; }

        public int Type { get; set; }
    }
}