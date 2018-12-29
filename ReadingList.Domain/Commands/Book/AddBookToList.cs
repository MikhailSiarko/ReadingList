namespace ReadingList.Domain.Commands
{
    public class AddBookToList : SecuredCommand
    {
        public readonly int BookId;

        public readonly int ListId;
        
        public AddBookToList(int userId, int bookId, int listId) : base(userId)
        {
            BookId = bookId;
            ListId = listId;
        }
    }
}