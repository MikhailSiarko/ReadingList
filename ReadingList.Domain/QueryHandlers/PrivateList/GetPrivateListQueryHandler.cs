using System.Threading.Tasks;
using ReadingList.Domain.Queries.PrivateList;
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
                    @"SELECT l.OwnerId,
                             i.Id,
                             i.BookId,
                             i.ReadingTimeInTicks,
                             b.Title,
                             b.Author,
                             (SELECT s.Name FROM BookItemStatuses as s WHERE s.Id = i.StatusId) as Status
                    FROM BookLists AS l
					LEFT JOIN (SELECT li.Id, li.BookId, li.BookListId, li.StatusId, li.ReadingTimeInTicks FROM BookListItems li WHERE li.Discriminator = 'PrivateBookListItem') AS i ON i.BookListId = l.OwnerId
                    LEFT JOIN Books AS b ON i.BookId = b.Id
					WHERE l.Discriminator = 'PrivateBookList' AND l.OwnerId = (SELECT u.Id FROM Users AS u WHERE u.Login = @login);",
                    (list, item) =>
                    {
                        list.Items.Add(item);
                        return list;
                    }, new { login = query.Login });
        }
    }
}