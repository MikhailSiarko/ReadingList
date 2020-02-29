using GraphQL.Types;
using ReadingList.Models.Read;

namespace ReadingList.Api.GraphQL.Types
{
    public class PrivateBookListItemType : ObjectGraphType<PrivateBookListItemDto>
    {
        public PrivateBookListItemType()
        {
            Field(x => x.Id);
            Field(x => x.Title);
            Field(x => x.BookId);
            Field(x => x.Status);
            Field(x => x.ListId);
            Field(x => x.ReadingTimeInSeconds);
            Field(x => x.Author);
            Field(x => x.Genre);
        }
    }
}