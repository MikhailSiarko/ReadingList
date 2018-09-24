using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Infrastructure.Filters
{
    public static class BookListItemFilterExpressions
    {
        public static FilterExpression<TItem> ItemBelongsToList<TItem>(int listId) where TItem : BookListItemWm
        {
            return FilterExpression<TItem>.Create(item => item.BookListId == listId);
        }
    }
}
