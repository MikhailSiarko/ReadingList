namespace ReadingList.WriteModel.Models.HelpEntities
{
    public class SharedBookListTagWm
    {
        public BookListWm SharedBookList { get; set; }

        public int SharedBookListId { get; set; }

        public TagWm Tag { get; set; }

        public int TagId { get; set; }
    }
}