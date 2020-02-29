using GraphQL.Types;
using ReadingList.Api.RequestData;

namespace ReadingList.Api.GraphQL.InputTypes
{
    public class LoginInputType : InputObjectGraphType<LoginRequestData>
    {
        public LoginInputType()
        {
            Name = "LoginInput";
            Field(x => x.Email);
            Field(x => x.Password);
        }
    }
}