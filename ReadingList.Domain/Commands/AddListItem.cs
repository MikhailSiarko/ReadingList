namespace ReadingList.Domain.Commands
{
    public abstract class AddListItem<TListItemDto> : SecuredCommand<TListItemDto>
    {
        public readonly int BookId;

        protected AddListItem(int userId, int bookId) : base(userId)
        {
            BookId = bookId;
        }
    }
}