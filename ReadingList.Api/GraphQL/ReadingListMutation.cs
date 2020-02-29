using GraphQL.Types;
using ReadingList.Api.GraphQL.MutationTypes;

namespace ReadingList.Api.GraphQL
{
    public class ReadingListMutation : ObjectGraphType
    {
        public ReadingListMutation()
        {
            Field<AccountMutation>("register", resolve: _ => new { });
            Field<BooksMutation>("addToLists", resolve: _ => new { });
        }
    }
}