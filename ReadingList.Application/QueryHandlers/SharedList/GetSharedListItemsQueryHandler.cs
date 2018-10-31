using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Queries;
using ReadingList.Read;

namespace ReadingList.Application.QueryHandlers
{
    public class GetSharedListItemsQueryHandler : QueryHandler<GetSharedListItemsQuery, IEnumerable<SharedBookListItemDto>>
    {
        public GetSharedListItemsQueryHandler(IApplicationDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<IEnumerable<SharedBookListItemDto>> Handle(GetSharedListItemsQuery query)
        {
            return await DbConnection.QueryAsync(query.SqlQueryContext,
                       async reader =>
                       {
                           var items = new List<SharedBookListItemDto>(await reader.ReadAsync<SharedBookListItemDto>());

                           var tags = (await reader.ReadAsync<(string TagName, int? ItemId)>()).ToList();

                           foreach (var itemRm in items)
                           {
                               itemRm.Tags =
                                   tags.Where(t => t.ItemId.HasValue && t.ItemId.Value == itemRm.Id)
                                       .Select(x => x.TagName).ToList();
                           }

                           return items;
                       }) ??
                   new List<SharedBookListItemDto>();
        }
    }
}