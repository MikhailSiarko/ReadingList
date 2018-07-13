using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.Domain.Services.Validation;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using ListRM = ReadingList.ReadModel.Models.PrivateBookList;
using ItemRM = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, PrivateBookListDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IPrivateBookListSqlService _privateBookListSqlService;

        public GetPrivateListQueryHandler(IReadDbConnection dbConnection, IPrivateBookListSqlService privateBookListSqlService)
        {
            _dbConnection = dbConnection;
            _privateBookListSqlService = privateBookListSqlService;
        }

        protected override async Task<PrivateBookListDto> Handle(GetPrivateListQuery query)
        {
            var listDictionary = new Dictionary<int, ListRM>();

            var privateList =
                await _dbConnection.QueryFirstAsync<ListRM, ItemRM, ListRM>(
                    _privateBookListSqlService.GetPrivateBookListSqlQuery(),
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
                    }, new {login = query.UserLogin, type = (int) BookListType.Private});

            EntitiesValidator.Validate(privateList,
                new OnNotExistExceptionData(typeof(ListRM), new {email = query.UserLogin}));

            return Mapper.Map<ListRM, PrivateBookListDto>(privateList);
        }
    }
}