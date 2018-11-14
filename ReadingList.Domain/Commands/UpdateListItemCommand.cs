namespace ReadingList.Domain.Commands
{
    public abstract class UpdateListItemCommand<TItemDto> : UpdateCommand<TItemDto>
    {
        public readonly int ItemId;
        
        protected UpdateListItemCommand(int userId, int itemId) : base(userId)
        {
            ItemId = itemId;
        }
    }
}