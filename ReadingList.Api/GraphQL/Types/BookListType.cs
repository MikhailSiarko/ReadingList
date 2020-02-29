using GraphQL.Types;
using ReadingList.Models.Read.Abstractions;

namespace ReadingList.Api.GraphQL.Types
{
    public class BookListType : ObjectGraphType<BookListDto>
    {
        public BookListType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Type);
            Field(x => x.OwnerLogin);
            Field(x => x.OwnerId);
        }
    }
}