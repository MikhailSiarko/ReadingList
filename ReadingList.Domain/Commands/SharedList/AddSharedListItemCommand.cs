using ReadingList.Domain.Commands.PrivateList;

namespace ReadingList.Domain.Commands.SharedList
{
    public class AddSharedListItemCommand : AddPrivateItemCommand
    {
        public readonly int ListId;
        
        public AddSharedListItemCommand(int listId, string login, string title, string author) : base(login, title, author)
        {
            ListId = listId;
        }
    }
}