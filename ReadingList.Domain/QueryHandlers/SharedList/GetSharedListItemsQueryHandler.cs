using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers.SharedList
{
    public class GetSharedListItemsQueryHandler : QueryHandler<GetSharedListItemsQuery, IEnumerable<SharedBookListItemDto>>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly ISharedBookListSqlService _listSqlService;

        public GetSharedListItemsQueryHandler(IReadDbConnection dbConnection, ISharedBookListSqlService listSqlService)
        {
            _dbConnection = dbConnection;
            _listSqlService = listSqlService;
        }

        protected override async Task<IEnumerable<SharedBookListItemDto>> Handle(GetSharedListItemsQuery query)
        {
            var listItems = await _dbConnection.QueryMultipleAsync(_listSqlService.GetSharedListItemsSqlQuery(),
                async reader =>
                {
                    var list = new List<SharedBookListItemRm>();

                    list.AddRange(await reader.ReadAsync<SharedBookListItemRm>());

                    var tags = (await reader.ReadAsync<(string TagName, int? ItemId)>()).ToList();

                    foreach (var itemRm in list)
                    {
                        itemRm.Tags =
                            tags.Where(t => t.ItemId.HasValue && t.ItemId.Value == itemRm.Id)
                                .Select(x => x.TagName).ToList();
                    }

                    return list;
                },
                new {listId = query.ListId});

            return Mapper.Map<IEnumerable<SharedBookListItemRm>, IEnumerable<SharedBookListItemDto>>(listItems);
        }
    }
}