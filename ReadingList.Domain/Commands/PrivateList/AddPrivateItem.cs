using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class AddPrivateItem : AddListItem<PrivateBookListItemDto>
    {
        public AddPrivateItem(int userId, int bookId) : base(userId, bookId)
        {
        }
    }
}