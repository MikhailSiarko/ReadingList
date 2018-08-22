namespace ReadingList.ReadModel.Models
{
    public abstract class BookList : ReadEntity
    {
        public string Name { get; set; }

        public int OwnerId { get; set; }
    }
}
