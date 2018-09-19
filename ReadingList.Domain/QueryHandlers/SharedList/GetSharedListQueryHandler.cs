using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.Models;

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
            var sharedList =
                await _dbConnection.QuerySingleAsync<SharedBookListRm>(
                    _sharedBookListSqlService.GetBookListSqlQuery(),
                    async reader =>
                    {
                        var list = (await reader.ReadAsync<SharedBookListRm>()).SingleOrDefault();

                        if (list == null) 
                            return null;
                        
                        var tags = (await reader.ReadAsync<string>()).ToList();

                        list.Tags = tags;

                        return list;
                    }, new {id = query.ListId}) ??
                throw new ObjectNotExistException<SharedBookListRm>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = query.ListId.ToString()
                });

            return Mapper.Map<SharedBookListRm, SharedBookListDto>(sharedList);
        }
    }
}