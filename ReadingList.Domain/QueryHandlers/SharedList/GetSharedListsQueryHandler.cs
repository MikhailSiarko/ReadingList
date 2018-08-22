using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using SharedListRM = ReadingList.ReadModel.Models.SharedBookList;
using SharedItemRM = ReadingList.ReadModel.Models.SharedBookListItem;

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
            var sharedLists = await _dbConnection.QueryAsync<SharedListRM>(_sharedBookListSqlService.GetListsSqlQuery(),
                new {login = query.UserLogin}) ?? new List<SharedListRM>();

            return Mapper.Map<IEnumerable<SharedListRM>, IEnumerable<SharedBookListDto>>(sharedLists);
        }
    }
}