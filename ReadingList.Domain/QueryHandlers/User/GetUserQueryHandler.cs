using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using UserRM = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserRM>
    {
        protected override Task<UserRM> Process(GetUserQuery query)
        {
            return Task.Run(() => UserSource.GetSource().SingleOrDefault(u => u.Id == query.UserId));
        }
    }
}