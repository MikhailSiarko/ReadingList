using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetSharedListQueryHandler : QueryHandler<GetSharedListQuery, SharedListDto>
    {
        private readonly IDbReader _dbConnection;
        private readonly ISharedBookListSqlService _sharedBookListSqlService;

        public GetSharedListQueryHandler(IDbReader dbConnection, ISharedBookListSqlService sharedBookListSqlService)
        {
            _dbConnection = dbConnection;
            _sharedBookListSqlService = sharedBookListSqlService;
        }
        
        protected override async Task<SharedListDto> Handle(GetSharedListQuery query)
        {
            var sharedList =
                await _dbConnection.QuerySingleAsync(
                    _sharedBookListSqlService.GetBookListSqlQuery(),
                    async reader =>
                    {
                        var list = (await reader.ReadAsync<SharedBookListRm>()).SingleOrDefault();

                        if (list == null) 
                            return null;
                        
                        var tags = (await reader.ReadAsync<string>()).ToList();

                        list.Tags = tags;

                        var items = new List<SharedBookListItemRm>(await reader.ReadAsync<SharedBookListItemRm>());

                        var itemsTags = (await reader.ReadAsync<(string TagName, int? ItemId)>()).ToList();

                        foreach (var itemRm in items)
                        {
                            itemRm.Tags =
                                itemsTags.Where(t => t.ItemId.HasValue && t.ItemId.Value == itemRm.Id)
                                    .Select(x => x.TagName).ToList();
                        }

                        list.Items = items;

                        return list;
                    }, new {listId = query.ListId}) ??
                throw new ObjectNotExistException<SharedBookListRm>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = query.ListId.ToString()
                });

            return Mapper.Map<SharedBookListRm, SharedListDto>(sharedList);
        }
    }
}