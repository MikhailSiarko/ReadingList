namespace ReadingList.Domain.Commands
{
    public abstract class AddListItemCommand<TListItemDto> : SecuredCommand<TListItemDto>
    {
        public readonly int BookId;

        protected AddListItemCommand(int userId, int bookId) : base(userId)
        {
            BookId = bookId;
        }
    }
}