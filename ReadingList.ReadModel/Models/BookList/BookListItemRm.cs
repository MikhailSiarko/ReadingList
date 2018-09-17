namespace ReadingList.ReadModel.Models
{
    public abstract class BookListItemRm : EntityRm
    {
        public int ListId { get; set; }
        
        public string Title { get; set; }

        public string Author { get; set; }
    }
}