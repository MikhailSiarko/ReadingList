using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers.SharedList
{
    public class GetSharedListsQueryHandler : QueryHandler<GetSharedListsQuery, IEnumerable<SharedBookListDto>>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly ISharedBookListSqlService _sharedBookListSqlService;

        public GetSharedListsQueryHandler(IReadDbConnection dbConnection, ISharedBookListSqlService sharedBookListSqlService)
        {
            _dbConnection = dbConnection;
            _sharedBookListSqlService = sharedBookListSqlService;
        }
        
        protected override async Task<IEnumerable<SharedBookListDto>> Handle(GetSharedListsQuery query)
        {
            var sharedLists = await _dbConnection.QueryAsync<SharedBookListRm>(_sharedBookListSqlService.GetListsSqlQuery(),
                new {login = query.UserLogin}) ?? new List<SharedBookListRm>();

            return Mapper.Map<IEnumerable<SharedBookListRm>, IEnumerable<SharedBookListDto>>(sharedLists);
        }
    }
}