using GraphQL.Types;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Api.GraphQL.Types
{
    public class BookStatusSelectListItemType : ObjectGraphType<SelectListItem<int>>
    {
        public BookStatusSelectListItemType()
        {
            Field(x => x.Text);
            Field(x => x.Value);
        }
    }
}
