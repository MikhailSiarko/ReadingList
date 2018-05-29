using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using ListRM = ReadingList.ReadModel.Models.PrivateBookList;
using ItemRM = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, ListRM>
    {
        private readonly IReadDbConnection _dbConnection;

        public GetPrivateListQueryHandler(IReadDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override async Task<ListRM> Process(GetPrivateListQuery query)
        {
            var listDictionary = new Dictionary<int, ListRM>();
            var sqlBuilder = new SqlBuilder();
            sqlBuilder
                .Select("l.Id, l.Name, l.OwnerId, i.Id, i.ReadingTimeInTicks, i.Title, i.Author, i.Status")
                .LeftJoin(
                    "(SELECT li.Id, li.Title, li.Author, li.BookListId, li.Status, li.ReadingTimeInTicks FROM PrivateBookListItems li)")
                .Where("l.OwnerId = (SELECT u.Id FROM Users AS u WHERE u.Login = @login) AND l.Type = @type")
                .AddParameters(new { login = query.Login, type = (int)BookListType.Private });
            var selectPrivateListBuilder =
                sqlBuilder.AddTemplate(
                    "SELECT /**select**/ FROM BookLists AS l /**leftjoin**/ AS i ON i.BookListId = l.Id /**where**/");
            return 
                await _dbConnection.QueryFirstAsync<ListRM, ItemRM, ListRM>(selectPrivateListBuilder.RawSql,
                    (list, item) =>
                    {
                        if (!listDictionary.TryGetValue(list.Id, out var listEntry))
                        {
                            listEntry = list;
                            listEntry.Items = new List<ItemRM>();
                            listDictionary.Add(listEntry.Id, list);
                        }
        
                        if(item != null)
                            listEntry.Items.Add(item);
                        return listEntry;
                    }, selectPrivateListBuilder.Parameters);
        }
    }
}