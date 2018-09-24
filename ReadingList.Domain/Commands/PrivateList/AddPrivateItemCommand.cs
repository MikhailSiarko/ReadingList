using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public class AddPrivateItemCommand : AddListItemCommand
    {
        public AddPrivateItemCommand(string login, BookInfo bookInfo) : base(login, bookInfo)
        {
        }
    }
}