using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using SharedListRM = ReadingList.ReadModel.Models.SharedBookList;
using SharedItemRM = ReadingList.ReadModel.Models.SharedBookListItem;

namespace ReadingList.Domain.QueryHandlers.SharedList
{
    public class GetSharedListQueryHandler : QueryHandler<GetSharedListQuery, SharedBookListDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly ISharedBookListSqlService _sharedBookListSqlService;

        public GetSharedListQueryHandler(IReadDbConnection dbConnection, ISharedBookListSqlService sharedBookListSqlService)
        {
            _dbConnection = dbConnection;
            _sharedBookListSqlService = sharedBookListSqlService;
        }
        
        protected override async Task<SharedBookListDto> Handle(GetSharedListQuery query)
        {
            var listDictionary = new Dictionary<int, SharedListRM>();

            var privateList =
                await _dbConnection.QueryFirstAsync<SharedListRM, SharedItemRM, SharedListRM>(
                    _sharedBookListSqlService.GetBookListSqlQuery(),
                    (list, item) =>
                    {
                        if (!listDictionary.TryGetValue(list.Id, out var listEntry))
                        {
                            listEntry = list;
                            listEntry.Items = new List<SharedItemRM>();
                            listDictionary.Add(listEntry.Id, list);
                        }

                        if (item != null)
                            listEntry.Items.Add(item);
                        return listEntry;
                    }, new {id = query.ListId}) ??
                throw new ObjectNotExistException<SharedListRM>(new {id = query.ListId});

            return Mapper.Map<SharedListRM, SharedBookListDto>(privateList);
        }
    }
}