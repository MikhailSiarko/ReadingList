using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using ReadingList.Api.GraphQL.MutationTypes;

namespace ReadingList.Api.GraphQL
{
    public class ReadingListMutation : ObjectGraphType
    {
        public ReadingListMutation()
        {
            Field<AccountMutation>("account", resolve: _ => new { });
            Field<BookMutation>("book", resolve: _ => new { }).RequiresAuthorization();
        }
    }
}