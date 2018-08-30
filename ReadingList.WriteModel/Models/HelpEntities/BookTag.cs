namespace ReadingList.WriteModel.Models.HelpEntities
{
    public class BookTagWm
    {
        public int BookId { get; set; }

        public BookWm Book { get; set; }

        public int TagId { get; set; }

        public TagWm Tag { get; set; }
    }
}
