using GraphQL.Types;
using ReadingList.Api.RequestData;

namespace ReadingList.Api.GraphQL.InputTypes
{
    public class AllBooksInputType : InputObjectGraphType<GetBooksRequestData>
    {
        public AllBooksInputType()
        {
            Name = "AllBooksInput";
            Field(x => x.Query, true);
            Field(x => x.Chunk, true);
            Field(x => x.Count, true);
        }
    }
}