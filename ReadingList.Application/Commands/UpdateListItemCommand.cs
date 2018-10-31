using ReadingList.Application.Infrastructure;

namespace ReadingList.Application.Commands
{
    public abstract class UpdateListItemCommand<TItemDto> : UpdateCommand<TItemDto>
    {
        public readonly int ItemId;
        
        public readonly BookInfo BookInfo;
        
        protected UpdateListItemCommand(string userLogin, int itemId, BookInfo bookInfo) : base(userLogin)
        {
            ItemId = itemId;
            BookInfo = bookInfo;
        }
    }
}