using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.FluentSqlBuilder;
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

            var sqlResult = FluentSqlBuilder.NewBuilder()
                .Select("l.Id, l.Name, l.OwnerId, i.Id, i.ReadingTimeInTicks, i.Title, i.Author, i.Status")
                .From("BookLists AS l")
                .LeftJoin(
                    "(SELECT li.Id, li.Title, li.Author, li.BookListId, li.Status, li.ReadingTimeInTicks FROM PrivateBookListItems li) AS i ON i.BookListId = l.Id")
                .Where("l.OwnerId = (SELECT u.Id FROM Users AS u WHERE u.Login = @login) AND l.Type = @type")
                .AddParameters(new {login = query.Login, type = (int) BookListType.Private})
                .Build();
            
            return 
                await _dbConnection.QueryFirstAsync<ListRM, ItemRM, ListRM>(sqlResult.RawSql,
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
                    }, sqlResult.Parameters);
        }
    }
}