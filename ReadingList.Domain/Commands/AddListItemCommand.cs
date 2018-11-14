using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public abstract class AddListItemCommand<TListItemDto> : SecuredCommand<TListItemDto>
    {
        public readonly BookInfo BookInfo;

        protected AddListItemCommand(int userId, BookInfo bookInfo) : base(userId)
        {
            BookInfo = bookInfo;
        }
    }
}