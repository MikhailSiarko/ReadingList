using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public abstract class AddListItemCommand<TListItemDto> : SecuredCommand<TListItemDto>
    {
        public readonly BookInfo BookInfo;

        protected AddListItemCommand(string userLogin, BookInfo bookInfo) : base(userLogin)
        {
            BookInfo = bookInfo;
        }
    }
}