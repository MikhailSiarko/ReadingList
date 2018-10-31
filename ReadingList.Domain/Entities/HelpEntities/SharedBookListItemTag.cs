namespace ReadingList.Domain.Entities.HelpEntities
{
    public class SharedBookListItemTag
    {
        public int SharedBookListItemId { get; set; }

        public SharedBookListItem SharedBookListItem { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
