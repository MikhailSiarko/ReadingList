using ReadingList.Application.Infrastructure;

namespace ReadingList.Application.Commands
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