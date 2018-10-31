namespace ReadingList.Domain.Entities.HelpEntities
{
    public class SharedBookListTag
    {
        public BookList SharedBookList { get; set; }

        public int SharedBookListId { get; set; }

        public Tag Tag { get; set; }

        public int TagId { get; set; }
    }
}