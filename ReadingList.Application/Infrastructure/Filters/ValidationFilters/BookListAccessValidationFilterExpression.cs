using System.Linq;
using ReadingList.Domain.Entities;

namespace ReadingList.Application.Infrastructure.Filters.ValidationFilters
{
    public static class BookListAccessValidationFilterExpression
    {
        public static FilterExpression<BookList> UserIsOwner(string username)
        {
            return FilterExpression<BookList>.Create(list => list.Owner.Login == username);
        }

        public static FilterExpression<TItem> UserIsOwnerOfItem<TItem>(string username) where TItem : BookListItem
        {
            return FilterExpression<TItem>.Create(item => item.BookList.Owner.Login == username);
        }

        public static FilterExpression<BookList> UserIsModerator(string username)
        {
            return FilterExpression<BookList>.Create(list => list.BookListModerators.Any(m => m.User.Login == username));
        }

        public static FilterExpression<BookList> UserIsOwnerOrModerator(string username)
        {
            return UserIsOwner(username) || UserIsModerator(username);
        }
    }
}