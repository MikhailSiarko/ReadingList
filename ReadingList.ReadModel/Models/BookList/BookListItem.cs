namespace ReadingList.ReadModel.Models
{
    public abstract class BookListItem : ReadEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }
}