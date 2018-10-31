using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure;

namespace ReadingList.Application.Commands
{
    public class UpdatePrivateListItemCommand : UpdateListItemCommand<PrivateBookListItemDto>
    {
        public readonly int Status;
        
        public UpdatePrivateListItemCommand(string login, int itemId, BookInfo bookInfo, int status) : base(login, itemId, bookInfo)
        {
            Status = status;
        }
    }
}