using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel;
using ListRM = ReadingList.ReadModel.BookList.Models.PrivateBookList;
using ItemRM = ReadingList.ReadModel.BookList.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, ListRM>
    {
        private readonly ReadingListConnection _connection;

        public GetPrivateListQueryHandler(ReadingListConnection connection)
        {
            _connection = connection;
        }

        protected override async Task<ListRM> Process(GetPrivateListQuery query)
        {
            return 
                await _connection.QuerySingle<ListRM, ItemRM, ListRM>(
                    @"SELECT
                          l.Id,
                          l.JsonFields,
                          l.OwnerId,
                          i.Id,
                          i.ReadingTimeInTicks,
                          i.Title,
                          i.Author
                      FROM BookLists AS l
                        LEFT JOIN (SELECT li.Id, li.Title, li.Author, li.BookListId, li.Status, li.ReadingTimeInTicks FROM PrivateBookListItems li) AS i ON i.BookListId = l.OwnerId
                      WHERE l.OwnerId = (SELECT u.Id FROM Users AS u WHERE u.Login = @login);",
                    (list, item) =>
                    {
                        list.Items.Add(item);
                        return list;
                    }, new { login = query.Login });
        }
    }
}