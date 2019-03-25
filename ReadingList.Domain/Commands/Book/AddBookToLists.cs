using System.Collections.Generic;

namespace ReadingList.Domain.Commands
{
    public class AddBookToLists : SecuredCommand
    {
        public readonly int BookId;

        public readonly IEnumerable<int> ListsIds;

        public AddBookToLists(int userId, int bookId, IEnumerable<int> listIds) : base(userId)
        {
            BookId = bookId;
            ListsIds = listIds;
        }
    }
}