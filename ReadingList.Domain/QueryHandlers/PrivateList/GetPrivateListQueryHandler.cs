using System.Collections.Generic;
using System.Threading.Tasks;
using Awesome.Data.Sql.Builder;
using Awesome.Data.Sql.Builder.Renderers;
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

        protected override async Task<ListRM> Handle(GetPrivateListQuery query)
        {
            var listDictionary = new Dictionary<int, ListRM>();

            var sql = SqlStatements.Select("l.Id", "l.Name", "l.OwnerId", "i.Id", "i.ReadingTimeInTicks", "i.Title",
                    "i.Author", "i.Status")
                .From("BookLists AS l")
                .LeftOuterJoin(
                    new TableClause(
                            "(SELECT Id, Title, Author, BookListId, Status, ReadingTimeInTicks FROM PrivateBookListItems)")
                        .As("i"), "i.BookListId = l.Id")
                .Where("l.OwnerId = (SELECT Id FROM Users WHERE Login = @login)")
                .Where("l.Type = @type")
                .ToSql(new SqlServerSqlRenderer());
            return 
                await _dbConnection.QueryFirstAsync<ListRM, ItemRM, ListRM>(sql,
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
                    }, new {login = query.Login, type = (int) BookListType.Private});
        }
    }
}