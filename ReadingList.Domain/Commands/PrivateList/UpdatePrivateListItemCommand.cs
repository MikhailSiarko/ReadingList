using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class UpdatePrivateListItemCommand : UpdateListItemCommand<PrivateBookListItemDto>
    {
        public readonly int Status;

        public UpdatePrivateListItemCommand(int userId, int itemId, int status) : base(userId, itemId)
        {
            Status = status;
        }
    }
}