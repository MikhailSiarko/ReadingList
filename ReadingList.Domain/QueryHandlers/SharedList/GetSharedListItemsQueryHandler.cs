using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetSharedListItemsQueryHandler : QueryHandler<GetSharedListItemsQuery, IEnumerable<SharedBookListItemDto>>
    {
        private readonly IDbReader _dbConnection;
        private readonly ISharedBookListSqlService _listSqlService;

        public GetSharedListItemsQueryHandler(IDbReader dbConnection, ISharedBookListSqlService listSqlService)
        {
            _dbConnection = dbConnection;
            _listSqlService = listSqlService;
        }

        protected override async Task<IEnumerable<SharedBookListItemDto>> Handle(GetSharedListItemsQuery query)
        {
            var listItems = await _dbConnection.QueryAsync(_listSqlService.GetSharedListItemsSqlQuery(),
                async reader =>
                {
                    var items = new List<SharedBookListItemRm>(await reader.ReadAsync<SharedBookListItemRm>());

                    var tags = (await reader.ReadAsync<(string TagName, int? ItemId)>()).ToList();

                    foreach (var itemRm in items)
                    {
                        itemRm.Tags =
                            tags.Where(t => t.ItemId.HasValue && t.ItemId.Value == itemRm.Id)
                                .Select(x => x.TagName).ToList();
                    }

                    return items;
                },
                new {listId = query.ListId});

            return Mapper.Map<IEnumerable<SharedBookListItemRm>, IEnumerable<SharedBookListItemDto>>(listItems);
        }
    }
}