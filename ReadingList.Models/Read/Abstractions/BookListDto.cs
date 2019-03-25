namespace ReadingList.Models.Read.Abstractions
{
    public class BookListDto : Entity
    {
        public string Name { get; set; }

        public int OwnerId { get; set; }

        public string OwnerLogin { get; set; }

        public int Type { get; set; }
    }
}