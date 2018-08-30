namespace ReadingList.WriteModel.Models.HelpEntities
{
    public class SharedBookListItemTagWm
    {
        public int SharedBookListItemId { get; set; }

        public SharedBookListItemWm SharedBookListItem { get; set; }

        public int TagId { get; set; }

        public TagWm Tag { get; set; }
    }
}
