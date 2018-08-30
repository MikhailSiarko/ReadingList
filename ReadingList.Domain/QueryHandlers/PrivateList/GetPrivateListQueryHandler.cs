using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, PrivateBookListDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IBookListSqlService _bookListSqlService;

        public GetPrivateListQueryHandler(IReadDbConnection dbConnection, IBookListSqlService bookListSqlService)
        {
            _dbConnection = dbConnection;
            _bookListSqlService = bookListSqlService;
        }

        protected override async Task<PrivateBookListDto> Handle(GetPrivateListQuery query)
        {
            var listDictionary = new Dictionary<int, PrivateBookListRm>();

            var privateList =
                await _dbConnection.QueryFirstAsync<PrivateBookListRm, PrivateBookListItemRm, PrivateBookListRm>(
                    _bookListSqlService.GetBookListSqlQuery(),
                    (list, item) =>
                    {
                        if (!listDictionary.TryGetValue(list.Id, out var listEntry))
                        {
                            listEntry = list;
                            listEntry.Items = new List<PrivateBookListItemRm>();
                            listDictionary.Add(listEntry.Id, list);
                        }

                        if (item != null)
                            listEntry.Items.Add(item);
                        return listEntry;
                    }, new {login = query.UserLogin}) ??
                throw new ObjectNotExistException<PrivateBookListRm>(new {email = query.UserLogin});

            return Mapper.Map<PrivateBookListRm, PrivateBookListDto>(privateList);
        }
    }
}