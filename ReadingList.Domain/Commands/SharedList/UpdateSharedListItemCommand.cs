using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedListItemCommand : UpdateListItemCommand<SharedBookListItemDto>
    {
        public readonly int ListId;

        public UpdateSharedListItemCommand(int userId, int itemId, int listId)
            : base(userId, itemId)
        {
            ListId = listId;
        }
    }
}