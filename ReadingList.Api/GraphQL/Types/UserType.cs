using GraphQL.Types;
using ReadingList.Models.Read;

namespace ReadingList.Api.GraphQL.Types
{
    public class UserType : ObjectGraphType<UserIdentityDto>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.ProfileId);
            Field(x => x.Role);
        }
    }
}