using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public class UpdatePrivateListItemCommand : UpdateListItemCommand
    {
        public readonly int Status;
        
        public UpdatePrivateListItemCommand(string login, int itemId, BookInfo bookInfo, int status) : base(login, itemId, bookInfo)
        {
            Status = status;
        }
    }
}