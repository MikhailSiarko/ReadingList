using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using ReadingList.Api.Extensions;
using ReadingList.Api.GraphQL.Types;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.GraphQL.QueryTypes
{
    public class BookListsQuery : ObjectGraphType
    {
        public BookListsQuery(IDomainService domainService, IHttpContextAccessor contextAccessor)
        {
            Name = "BookListsQuery";
            FieldAsync<ListGraphType<BookListType>>(
                "moderated",
                resolve: async ctx =>
                    await domainService.AskAsync(new GetModeratedLists(contextAccessor.HttpContext.User.GetUserId()))
            );
            FieldAsync<PrivateBookListType>(
                "private",
                resolve: async ctx =>
                    await domainService.AskAsync(new GetPrivateList(contextAccessor.HttpContext.User.GetUserId()))
            );
        }
    }
}