using ReadingList.Domain.Entities;

namespace ReadingList.Application.Infrastructure.Filters
{
    public static class BookListItemFilterExpressions
    {
        public static FilterExpression<TItem> BelongsToListWithId<TItem>(int listId) where TItem : BookListItem
        {
            return FilterExpression<TItem>.Create(item => item.BookListId == listId);
        }
    }
}
