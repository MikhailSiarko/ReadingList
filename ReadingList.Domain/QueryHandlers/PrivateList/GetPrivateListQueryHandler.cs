using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel;
using ReadingList.WriteModel.Models;
using ListRM = ReadingList.ReadModel.BookList.Models.PrivateBookList;
using ItemRM = ReadingList.ReadModel.BookList.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, ListRM>
    {
        private readonly ReadDbConnection _dbConnection;

        public GetPrivateListQueryHandler(ReadDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override async Task<ListRM> Process(GetPrivateListQuery query)
        {
            var lsitDictionary = new Dictionary<int, ListRM>();
            return 
                await _dbConnection.QueryFirst<ListRM, ItemRM, ListRM>(
                    @"SELECT
                      l.Id,
                      l.Name,
                      l.OwnerId,
                      i.Id,
                      i.ReadingTimeInTicks,
                      i.Title,
                      i.Author,
                      i.Status
                    FROM BookLists AS l 
                      LEFT JOIN (SELECT li.Id, li.Title, li.Author, li.BookListId, li.Status, li.ReadingTimeInTicks
                    FROM PrivateBookListItems li) AS i ON i.BookListId = l.Id
                    WHERE l.OwnerId = (SELECT u.Id FROM Users AS u WHERE u.Login = @login) AND l.Type = @type;",
                    (list, item) =>
                    {
                        if (!lsitDictionary.TryGetValue(list.Id, out var listEntry))
                        {
                            listEntry = list;
                            listEntry.Items = new List<ItemRM>();
                            lsitDictionary.Add(listEntry.Id, list);
                        }
        
                        if(item != null)
                            listEntry.Items.Add(item);
                        return listEntry;
                    }, new { login = query.Login, type = (int)BookListType.Private });
        }
    }
}