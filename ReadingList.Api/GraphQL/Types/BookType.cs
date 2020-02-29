using GraphQL.Types;
using ReadingList.Models.Read;

namespace ReadingList.Api.GraphQL.Types
{
    public class BookType : ObjectGraphType<BookDto>
    {
        public BookType()
        {
            Field(x => x.Id);
            Field(x => x.Author);
            Field(x => x.Title);
            Field(x => x.Genre);
            Field(x => x.Tags);
        }
    }
}