using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using ListRM = ReadingList.ReadModel.Models.PrivateBookList;
using ItemRM = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, PrivateBookListDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IBookListSqlService _bookListSqlService;

        public GetPrivateListQueryHandler(IReadDbConnection dbConnection, Func<BookListType, IBookListSqlService> sqlServiceAccessor)
        {
            _dbConnection = dbConnection;
            _bookListSqlService = sqlServiceAccessor(BookListType.Private);
        }

        protected override async Task<PrivateBookListDto> Handle(GetPrivateListQuery query)
        {
            var listDictionary = new Dictionary<int, ListRM>();

            var privateList =
                await _dbConnection.QueryFirstAsync<ListRM, ItemRM, ListRM>(
                    _bookListSqlService.GetBookListSqlQuery(),
                    (list, item) =>
                    {
                        if (!listDictionary.TryGetValue(list.Id, out var listEntry))
                        {
                            listEntry = list;
                            listEntry.Items = new List<ItemRM>();
                            listDictionary.Add(listEntry.Id, list);
                        }

                        if (item != null)
                            listEntry.Items.Add(item);
                        return listEntry;
                    }, new {login = query.UserLogin}) ??
                throw new ObjectNotExistException<ListRM>(new {email = query.UserLogin});

            return Mapper.Map<ListRM, PrivateBookListDto>(privateList);
        }
    }
}