using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands.SharedList
{
    public class AddSharedListItemCommand : AddListItemCommand
    {
        public readonly int ListId;

        public AddSharedListItemCommand(int listId, string login, BookInfo bookInfo) : base(login, bookInfo)
        {
            ListId = listId;
        }
    }
}