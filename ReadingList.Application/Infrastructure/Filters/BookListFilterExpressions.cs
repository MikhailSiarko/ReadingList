using ReadingList.Domain.Entities;
using ReadingList.Domain.Enumerations;

namespace ReadingList.Application.Infrastructure.Filters
{
    public static class BookListFilterExpressions
    {
        public static FilterExpression<BookList> SharedBookListWithId(int id)
        {
            return FilterExpression<BookList>.Create(EntityFilterExpressions.WithId<BookList>(id) &&
                                                       FilterExpression<BookList>.Create(list =>
                                                           list.Type == BookListType.Shared));
        }

        public static FilterExpression<BookList> PrivateBookListForUser(string ownerLogin)
        {
            return FilterExpression<BookList>.Create(list =>
                list.Owner.Login == ownerLogin && list.Type == BookListType.Private);
        }

        public static FilterExpression<BookList> SharedBookListsForUser(string login)
        {
            return FilterExpression<BookList>.Create(list => list.Owner.Login == login && list.Type == BookListType.Shared);
        }

        public static FilterExpression<BookList> SharedBookLists =>
            FilterExpression<BookList>.Create(list => list.Type == BookListType.Shared);
    }
}
