using GraphQL.Types;
using ReadingList.Api.GraphQL.InputTypes;
using ReadingList.Api.GraphQL.Types;
using ReadingList.Api.RequestData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Api.GraphQL.QueryTypes
{
    public class AccountQuery : ObjectGraphType
    {
        public AccountQuery(IDomainService domainService)
        {
            Name = "AccountQuery";
            FieldAsync<AuthenticationDataType>(
                "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> {Name = "input"}
                ),
                resolve: async context =>
                {
                    var input = context.GetArgument<LoginRequestData>("input");
                    return await domainService.AskAsync(new LoginUser(input.Email, input.Password));
                });
        }
    }
}