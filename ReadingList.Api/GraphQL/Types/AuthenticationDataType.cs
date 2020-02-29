using GraphQL.Types;
using ReadingList.Models.Read;

namespace ReadingList.Api.GraphQL.Types
{
    public class AuthenticationDataType : ObjectGraphType<AuthenticationDataDto>
    {
        public AuthenticationDataType()
        {
            Field(x => x.Token);
            Field<UserType>(
                "user",
                resolve: context => context.Source.User
                );
        }
    }
}