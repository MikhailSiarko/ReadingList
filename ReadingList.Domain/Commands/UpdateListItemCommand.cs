using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public abstract class UpdateListItemCommand : SecuredCommand
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