using GraphQL.Types;
using ReadingList.Api.GraphQL.InputTypes;
using ReadingList.Api.GraphQL.Types;
using ReadingList.Api.RequestData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Api.GraphQL.MutationTypes
{
    public class AccountMutation : ObjectGraphType
    {
        public AccountMutation(IDomainService domainService)
        {
            Name = "AccountMutation";
            FieldAsync<AuthenticationDataType>(
                "register",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RegisterInputType>> {Name = "input"}
                ),
                resolve: async context =>
                {
                    var input = context.GetArgument<RegisterRequestData>("input");
                    return await domainService.AskAsync(new RegisterUser(input.Email, input.Password, input.ConfirmPassword));
                });
        }
    }
}