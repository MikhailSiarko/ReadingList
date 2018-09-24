using System.Linq;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Infrastructure.Filters.ValidationFilters
{
    public static class BookListAccessValidationFilterExpression
    {
        public static FilterExpression<BookListWm> UserIsOwner(string username)
        {
            return FilterExpression<BookListWm>.Create(list => list.Owner.Login == username);
        }

        public static FilterExpression<TItem> UserIsOwnerOfItem<TItem>(string username) where TItem : BookListItemWm
        {
            return FilterExpression<TItem>.Create(item => item.BookList.Owner.Login == username);
        }

        public static FilterExpression<BookListWm> UserIsModerator(string username)
        {
            return FilterExpression<BookListWm>.Create(list => list.BookListModerators.Any(m => m.User.Login == username));
        }

        public static FilterExpression<BookListWm> UserIsOwnerOrModerator(string username)
        {
            return UserIsOwner(username) || UserIsModerator(username);
        }
    }
}