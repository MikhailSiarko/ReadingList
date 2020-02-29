using GraphQL.Types;
using ReadingList.Api.RequestData;

namespace ReadingList.Api.GraphQL.InputTypes
{
    public class AddBookToListsInputType : InputObjectGraphType<AddBookToListsRequestData>
    {
        public AddBookToListsInputType()
        {
            Name = "AddToListsInput";
            Field(x => x.Id);
            Field<NonNullGraphType<ListGraphType<IntGraphType>>>(
                "listIds",
                resolve: ctx => ctx.Source.ListIds
                );
        }
    }
}