using GraphQL.Types;
using ReadingList.Api.GraphQL.Types;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read.Queries;

namespace ReadingList.Api.GraphQL.QueryTypes
{
    public class BookStatusesQuery : ObjectGraphType
    {
        public BookStatusesQuery(IDomainService domainService)
        {
            Name = "BookStatusesQuery";
            FieldAsync<ListGraphType<SelectListItemType>>(
                "bookStatuses",
                resolve: async ctx => await domainService.AskAsync(new GetBookStatuses()));
        }
    }
}
