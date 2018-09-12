using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public class AddListItemCommand : SecuredCommand
    {
        public readonly BookInfo BookInfo;

        protected AddListItemCommand(string userLogin, BookInfo bookInfo) : base(userLogin)
        {
            BookInfo = bookInfo;
        }
    }
}