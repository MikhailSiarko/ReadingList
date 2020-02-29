using GraphQL.Types;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Api.GraphQL.Types
{
    public class SelectListItemType : ObjectGraphType<SelectListItem>
    {
        public SelectListItemType()
        {
            Field(x => x.Text);
            Field(x => x.Value);
        }
    }
}
