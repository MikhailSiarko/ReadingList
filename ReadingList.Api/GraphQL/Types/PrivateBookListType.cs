using GraphQL.Types;
using ReadingList.Models.Read;

namespace ReadingList.Api.GraphQL.Types
{
    public class PrivateBookListType : ObjectGraphType<PrivateBookListDto>
    {
        public PrivateBookListType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Type);
            Field(x => x.OwnerId);
            Field(x => x.OwnerLogin);
            Field<ListGraphType<PrivateBookListItemType>>(
                "items",
                resolve: ctx => ctx.Source.Items
                );
        }
    }
}