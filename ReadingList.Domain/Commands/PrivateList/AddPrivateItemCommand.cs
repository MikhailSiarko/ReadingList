using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands.PrivateList
{
    public class AddPrivateItemCommand : SecuredCommand
    {
        public readonly BookInfo BookInfo;

        public AddPrivateItemCommand(string login, BookInfo bookInfo) : base(login)
        {
            BookInfo = bookInfo;
        }
    }
}