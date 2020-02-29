using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using ReadingList.Api.GraphQL.InputTypes;
using ReadingList.Api.GraphQL.Types;
using ReadingList.Api.RequestData;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Read.Queries;
using ReadingList.Read.Queries.Book;

namespace ReadingList.Api.GraphQL.QueryTypes
{
    public class BooksQuery : ObjectGraphType
    {
        public BooksQuery(IDomainService domainService)
        {
            Name = "BooksQuery";
            FieldAsync<ChunkedListType<BookType, BookDto>>(
                "all",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AllBooksInputType>> {Name = "input"}
                ),
                resolve: async ctx =>
                {
                    var input = ctx.GetArgument<GetBooksRequestData>("input");
                    return await domainService.AskAsync(new FindBooks(input.Query, input.Chunk, input.Count));
                });
            FieldAsync<ListGraphType<BookStatusSelectListItemType>>(
                "statuses",
                resolve: async ctx => await domainService.AskAsync(new GetBookStatuses()));
        }
    }
}