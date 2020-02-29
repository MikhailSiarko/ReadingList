using GraphQL.Types;
using ReadingList.Api.RequestData;

namespace ReadingList.Api.GraphQL.InputTypes
{
    public class AddBookToListsInputType : InputObjectGraphType<AddBookToListsRequestData>
    {
        public AddBookToListsInputType()
        {
            Name = "AddToLists";
            Field(x => x.Id);
            Field(x => x.ListIds);
        }
    }
}