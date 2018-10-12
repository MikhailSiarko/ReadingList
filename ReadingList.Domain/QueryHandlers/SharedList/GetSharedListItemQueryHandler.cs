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
    public class GetSharedListItemQueryHandler : QueryHandler<GetSharedListItemQuery, SharedBookListItemDto>
    {
        private readonly IDbReader _readDbConnection;
        
        private readonly ISharedBookListSqlService _listSqlService;

        public GetSharedListItemQueryHandler(IDbReader readDbConnection, ISharedBookListSqlService listSqlService)
        {
            _readDbConnection = readDbConnection;
            _listSqlService = listSqlService;
        }

        protected override async Task<SharedBookListItemDto> Handle(GetSharedListItemQuery query)
        {
            var listItem = await _readDbConnection.QuerySingleAsync(
                               _listSqlService.GetBookListItemSqlQuery(),
                               async reader =>
                               {
                                   var item = (await reader.ReadAsync<SharedBookListItemRm>()).SingleOrDefault();

                                   if (item == null)
                                       return null;

                                   var tags = await reader.ReadAsync<string>();

                                   item.Tags = tags;

                                   return item;
                               },
                               new {itemId = query.ItemId, listId = query.ListId}) ??
                           throw new ObjectNotExistException<SimplifiedSharedBookListRm>(new OnExceptionObjectDescriptor
                           {
                               ["Item Id"] = query.ItemId.ToString()
                           });

            return Mapper.Map<SharedBookListItemRm, SharedBookListItemDto>(listItem);
        }
    }
}