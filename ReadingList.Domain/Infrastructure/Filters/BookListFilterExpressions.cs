using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Infrastructure.Filters
{
    public static class BookListFilterExpressions
    {
        public static FilterExpression<BookListWm> FindSharedBookList(int id)
        {
            return FilterExpression<BookListWm>.Create(EntityFilterExpressions.FindEntity<BookListWm>(id) &&
                                                       FilterExpression<BookListWm>.Create(list =>
                                                           list.Type == BookListType.Shared));
        }

        public static FilterExpression<BookListWm> FindPrivateBookList(string ownerLogin)
        {
            return FilterExpression<BookListWm>.Create(list =>
                list.Owner.Login == ownerLogin && list.Type == BookListType.Private);
        }

        public static FilterExpression<BookListWm> FindSharedBookListsForUser(int ownerId)
        {
            return FilterExpression<BookListWm>.Create(list => list.OwnerId == ownerId && list.Type == BookListType.Shared);
        }
    }
}
