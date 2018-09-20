using System.Linq;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Services.Validation
{
    public static class BookListAccessValidator
    {
        public static void Validate(string username, BookListWm listWm)
        {
            if (listWm.Owner != null && listWm.Owner.Login != username ||
                  listWm.BookListModerators != null && listWm.BookListModerators.All(x => x.User.Login != username))
                throw new AccessDeniedException();
        }
    }
}