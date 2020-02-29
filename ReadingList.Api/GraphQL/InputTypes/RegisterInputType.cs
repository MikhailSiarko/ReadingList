using GraphQL.Types;
using ReadingList.Api.RequestData;

namespace ReadingList.Api.GraphQL.InputTypes
{
    public class RegisterInputType : InputObjectGraphType<RegisterRequestData>
    {
        public RegisterInputType()
        {
            Name = "RegisterInput";
            Field(x => x.Email);
            Field(x => x.Password);
            Field(x => x.ConfirmPassword);
        }
    }
}