using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands.SharedList
{
    public class UpdateSharedListItemCommand : UpdateListItemCommand
    {
        public readonly int ListId;
        
        public UpdateSharedListItemCommand(string userLogin, int itemId, int listId, BookInfo bookInfo) : base(userLogin, itemId, bookInfo)
        {
            ListId = listId;
        }
    }
}