using ReadingList.Domain.Commands.PrivateList;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands.SharedList
{
    public class AddSharedListItemCommand : AddPrivateItemCommand
    {
        public readonly int ListId;
        
        public AddSharedListItemCommand(int listId, string login, BookInfo bookInfo) : base(login, bookInfo)
        {
            ListId = listId;
        }
    }
}