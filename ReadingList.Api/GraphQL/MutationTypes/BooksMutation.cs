using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using ReadingList.Api.GraphQL.InputTypes;
using ReadingList.Api.RequestData;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Api.Extensions;

namespace ReadingList.Api.GraphQL.MutationTypes
{
    public class BooksMutation : ObjectGraphType
    {
        public BooksMutation(IDomainService domainService, IHttpContextAccessor httpContextAccessor)
        {
            Name = "BooksMutation";
            FieldAsync<BooleanGraphType>(
                "addToLists",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AddBookToListsInputType>> {Name = "input"}
                ),
                resolve: async ctx =>
                {
                    var input = ctx.GetArgument<AddBookToListsRequestData>("input");
                    await domainService.ExecuteAsync(new AddBookToLists(httpContextAccessor.HttpContext.User.GetUserId(), input.Id, input.ListIds));
                    return true;
                });
        }
    }
}