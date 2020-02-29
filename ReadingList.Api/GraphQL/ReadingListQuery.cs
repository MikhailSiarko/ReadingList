using GraphQL.Types;
using ReadingList.Api.GraphQL.QueryTypes;

namespace ReadingList.Api.GraphQL
{
    public class ReadingListQuery : ObjectGraphType
    {
        public ReadingListQuery()
        {
            Field<AccountQuery>("account", resolve: _ => new { });
            Field<BooksQuery>("books", resolve: _ => new { });
            Field<BookStatusesQuery>("bookStatuses", resolve: _ => new { });
        }
    }
}
