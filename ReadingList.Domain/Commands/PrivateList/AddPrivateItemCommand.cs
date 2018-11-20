using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class AddPrivateItemCommand : AddListItemCommand<PrivateBookListItemDto>
    {
        public AddPrivateItemCommand(int userId, int bookId) : base(userId, bookId)
        {
        }
    }
}