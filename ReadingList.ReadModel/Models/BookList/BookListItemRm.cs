namespace ReadingList.ReadModel.Models
{
    public abstract class BookListItemRm : EntityRm
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }
}