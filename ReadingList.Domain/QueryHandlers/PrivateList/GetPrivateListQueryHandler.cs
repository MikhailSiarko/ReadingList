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
            return 
                await _dbConnection.QuerySingle<ListRM, ItemRM, ListRM>(
                    @"SELECT
                          l.Id,
                          l.Name,
                          l.OwnerId,
                          i.Id,
                          i.ReadingTimeInTicks,
                          i.Title,
                          i.Author
                      FROM (SELECT li.Id, li.Title, li.Author, li.BookListId, li.Status, li.ReadingTimeInTicks 
                        FROM PrivateBookListItems li) AS i
                      RIGHT OUTER JOIN BookLists AS l  ON i.BookListId = l.Id
                      WHERE l.OwnerId = (SELECT u.Id FROM Users AS u WHERE u.Login = @login) AND l.Type = @type;",
                    (list, item) =>
                    {
                        list.Items.Add(item);
                        return list;
                    }, new { login = query.Login, type = (int)BookListType.Private });
        }
    }
}