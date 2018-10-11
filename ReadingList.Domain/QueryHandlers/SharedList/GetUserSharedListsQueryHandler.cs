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
    public class GetUserSharedListsQueryHandler : QueryHandler<GetUserSharedListsQuery, IEnumerable<SharedBookListDto>>
    {
        private readonly IDbReader _dbConnection;
        private readonly ISharedBookListSqlService _sharedBookListSqlService;

        public GetUserSharedListsQueryHandler(IDbReader dbConnection, ISharedBookListSqlService sharedBookListSqlService)
        {
            _dbConnection = dbConnection;
            _sharedBookListSqlService = sharedBookListSqlService;
        }
        
        protected override async Task<IEnumerable<SharedBookListDto>> Handle(GetUserSharedListsQuery query)
        {
            var sharedLists =
                await _dbConnection.QueryAsync(_sharedBookListSqlService.GetUserBookListsSqlQuery(),
                    async reader =>
                    {
                        var lists = (await reader.ReadAsync<SharedBookListRm>()).ToList();

                        var tags = (await reader.ReadAsync<(string TagName, int ListId)>()).ToList();

                        foreach (var listRm in lists)
                        {
                            listRm.Tags = tags.Where(t => t.ListId == listRm.Id).Select(t => t.TagName);
                        }

                        return lists;
                    },
                    new {login = query.UserLogin}) ??
                new List<SharedBookListRm>();

            return Mapper.Map<IEnumerable<SharedBookListRm>, IEnumerable<SharedBookListDto>>(sharedLists);
        }
    }
}