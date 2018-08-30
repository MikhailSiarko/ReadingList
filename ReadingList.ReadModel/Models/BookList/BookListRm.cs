namespace ReadingList.ReadModel.Models
{
    public abstract class BookListRm : EntityRm
    {
        public string Name { get; set; }

        public int OwnerId { get; set; }
    }
}
