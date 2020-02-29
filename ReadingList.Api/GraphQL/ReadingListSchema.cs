using GraphQL;
using GraphQL.Types;

namespace ReadingList.Api.GraphQL
{
    public class ReadingListSchema : Schema
    {
        public ReadingListSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ReadingListQuery>();
            Mutation = resolver.Resolve<ReadingListMutation>();
        }
    }
}