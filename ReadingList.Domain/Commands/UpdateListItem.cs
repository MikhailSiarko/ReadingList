namespace ReadingList.Domain.Commands
{
    public abstract class UpdateListItem<TItemDto> : Update<TItemDto>
    {
        public readonly int ItemId;

        protected UpdateListItem(int userId, int itemId) : base(userId)
        {
            ItemId = itemId;
        }
    }
}