using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cinch.SqlBuilder;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using ListRM = ReadingList.ReadModel.Models.PrivateBookList;
using ItemRM = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, PrivateBookListDto>
    {
        private readonly IReadDbConnection _dbConnection;

        public GetPrivateListQueryHandler(IReadDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override async Task<PrivateBookListDto> Handle(GetPrivateListQuery query)
        {
            var listDictionary = new Dictionary<int, ListRM>();

            var sql = new SqlBuilder().Select("l.Id", "l.Name", "l.OwnerId", "i.Id", "i.ReadingTime", "i.Title",
                    "i.Author", "i.Status")
                .From("BookLists AS l")
                .LeftJoin("(SELECT Id, Title, Author, BookListId, Status, ReadingTime FROM PrivateBookListItems) AS i ON i.BookListId = l.Id")
                .Where("l.OwnerId = (SELECT Id FROM Users WHERE Login = @login)")
                .Where("l.Type = @type")
                .ToSql();

            var privateList =
                await _dbConnection.QueryFirstAsync<ListRM, ItemRM, ListRM>(sql,
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
            
            if(privateList == null)
                throw new ObjectNotExistException($"Private list for user {query.UserLogin}");

            return Mapper.Map<ListRM, PrivateBookListDto>(privateList);
        }
    }
}