using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using SharedListRM = ReadingList.ReadModel.Models.SharedBookList;
using SharedItemRM = ReadingList.ReadModel.Models.SharedBookListItem;

namespace ReadingList.Domain.QueryHandlers.SharedList
{
    public class GetSharedListQueryHandler : QueryHandler<GetSharedListQuery, SharedBookListDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IBookListSqlService _bookListSqlService;

        public GetSharedListQueryHandler(IReadDbConnection dbConnection, Func<BookListType, IBookListSqlService> sqlServiceAccessor)
        {
            _dbConnection = dbConnection;
            _bookListSqlService = sqlServiceAccessor(BookListType.Shared);
        }
        
        protected override async Task<SharedBookListDto> Handle(GetSharedListQuery query)
        {
            var listDictionary = new Dictionary<int, SharedListRM>();

            var privateList =
                await _dbConnection.QueryFirstAsync<SharedListRM, SharedItemRM, SharedListRM>(
                    _bookListSqlService.GetBookListSqlQuery(),
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