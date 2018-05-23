using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Queries.PrivateList;
using ReadingList.ReadModel.BookList.Models;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, PrivateBookList>
    {
        protected override Task<PrivateBookList> Process(GetPrivateListQuery query)
        {
            var user = UserSource.GetSource().Single(u => u.Email == query.Username);
            return Task.Run(() => new PrivateBookList {UserId = user.Id});
        }
    }
}